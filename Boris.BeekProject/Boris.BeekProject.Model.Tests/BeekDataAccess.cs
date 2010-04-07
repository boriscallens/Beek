using System.Configuration;
using System.Linq;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using Boris.Utils.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.DataAccess.Db4o;
using User=Boris.BeekProject.Model.Accounts.User;
using Db4objects.Db4o;
using System.IO;

namespace Boris.BeekProject.Model.Tests
{
    /// <summary>
    /// Summary description for DataAccess
    /// </summary>
    [TestClass]
    public class BeekDataAccess
    {
        private static IObjectServer beekServer;
        private static IBeekRepository beekRepos;
        private static IObjectServer userServer;
        private static IUserRepository userRepository;

        public TestContext TestContext { get; set; }

        public BeekDataAccess()
        {
            string beekPath = IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["beekRepository.path.db4o"]);
            string userPath = IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["userRepository.path.db4o"]);
            if (!File.Exists(beekPath))
            {
                new FileInfo(beekPath).Directory.Create();
            }
            if (!File.Exists(userPath))
            {
                new FileInfo(userPath).Directory.Create();
            }
            beekServer = Db4oFactory.OpenServer(beekPath, 0);
            userServer = Db4oFactory.OpenServer(userPath, 0);
            beekRepos = new BeekRepository(beekServer);
            userRepository = new UserRepository(userServer);
        }

       #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CanAddGenre()
        {
            var genre = new AlternateHistoryGenre();
            beekRepos.AddGenre(genre);
            Assert.IsTrue(beekRepos.GetGenres().Any(g=>g.Equals(genre)));
            beekRepos.AddGenre(genre);
            Assert.IsTrue(beekRepos.GetGenres().Count()==1);
            beekRepos.RemoveGenre(genre);
        }
        [TestMethod]
        public void CanRemoveGenre()
        {
            var genre = new MemoirGenre();
            beekRepos.AddGenre(genre);
            Assert.IsTrue(beekRepos.GetGenres().Any(g => g.Equals(genre)));
            beekRepos.RemoveGenre(genre);
            Assert.IsFalse(beekRepos.GetGenres().Any(g => g.Equals(genre)));
        }
        [TestMethod]
        public void CanUpdateGenre()
        {
            var genre = new BiographyGenre();
            beekRepos.AddGenre(genre);
            genre.Name = "updatedGenre";
            beekRepos.UpdateGenre(genre);
            int cnt = beekRepos.GetGenres().Count();
            Assert.IsTrue(beekRepos.GetGenres().Where(g => g.Name.Equals("updatedGenre")).Count() == cnt);
        }
    
        [TestMethod]
        public void CanAddBeek()
        {
            var beek = new BaseBeek(BeekTypes.ShortStory);
            beekRepos.AddBeek(beek);
            Assert.IsTrue(beekRepos.GetBeek().Contains(beek));
            beekRepos.RemoveBeek(beek);
        }
        [TestMethod]
        public void CanRemoveBeek()
        {
            var beek = new BaseBeek(BeekTypes.ShortStory);
            beekRepos.AddBeek(beek);
            Assert.IsTrue(beekRepos.GetBeek().Contains(beek));
            beekRepos.RemoveBeek(beek);
            Assert.IsTrue(!beekRepos.GetBeek().Contains(beek));
        }
        [TestMethod]
        public void CanUpdateBeek()
        {
            const string before = "before update";
            const string after = "after update";
            var beek = new BaseBeek(BeekTypes.LongStory){Title = before};
            beekRepos.AddBeek(beek);
            beek.Title = after;
            beekRepos.UpdateBeek(beek);
            Assert.IsFalse(beekRepos.GetBeek().Any(b=>b.Title.Equals(before)));
            Assert.IsTrue(beekRepos.GetBeek().Any(b => b.Title.Equals(after)));
        }
    
        [TestMethod]
        public void CanAddUser()
        {
            IUser user = GenerateTestUser();
            userRepository.AddUser(user);
            Assert.IsTrue(userRepository.GetUsers().Any(u => u.Equals(user)));
            userRepository.RemoveUser(user);
        }
        [TestMethod]
        public void CanDeleteUser()
        {
            IUser user = GenerateTestUser();
            userRepository.AddUser(user);
            Assert.IsTrue(userRepository.GetUsers().Any(u => u.Equals(user)));
            userRepository.RemoveUser(user);
            Assert.IsFalse(userRepository.GetUsers().Any(u => u.Equals(user)));
        }
        [TestMethod]
        public void CanUpdateUser()
        {
            IUser user = GenerateTestUser();
            const string after = "after";
            userRepository.AddUser(user);
            user.Name = after;
            user.AddRole(Roles.Illustrator);
            userRepository.UpdateUser(user);
            Assert.IsTrue(userRepository.GetUser(user.Id).Name.Equals(after));
            Assert.IsTrue(userRepository.GetUser(user.Id).IsInRole(Roles.Illustrator));
        }
        [TestMethod]
        public void UserStaysInRoleAfterAddingAndRetrieving()
        {
            IUser expected = GenerateTestUser();
            expected.AddRole(Roles.Anonymous);
            userRepository.AddUser(expected);
            IUser actual = userRepository.GetUser(expected.Id);
            Assert.IsTrue(actual.IsInRole(Roles.Anonymous));
        }
        [TestMethod]
        public void UpdatingUserAlsoUpdatesRoles()
        {
            // Generate a user without any roles
            IUser expected = GenerateTestUser();
            userRepository.AddUser(expected);

            // Get the previously saved user but now from this second server,
            // add a role to it and update to the new repos
            expected = userRepository.GetUser(expected.Id);
            expected.AddRole(Roles.Illustrator);
            userRepository.UpdateUser(expected);

            // Get the user again and check if the roles are updated correctly
            IUser actual = userRepository.GetUser(expected.Id);
            userRepository.GetUser(expected.Id);
        }
        [TestMethod]
        public void UpdatingUserAfterRemovingRoleDoesntCreateDuplicate()
        {
            IUser user = GenerateTestUser();
            user.AddRole(Roles.Anonymous);
            userRepository.AddUser(user);
            int expected = userRepository.GetUsers().Count();

            user.RemoveRole(Roles.Anonymous);
            userRepository.UpdateUser(user);
            Assert.IsTrue(expected == userRepository.GetUsers().Count());
        }

        private static IUser GenerateTestUser()
        {
            return new User("TestName", "TestPassword@123456", "test_test@test.com");
        }
    }
}
