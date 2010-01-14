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

namespace Boris.BeekProject.Model.Tests
{
    /// <summary>
    /// Summary description for DataAccess
    /// </summary>
    [TestClass]
    public class BeekDataAccess
    {
        private static readonly IObjectServer beekServer = Db4oFactory.OpenServer(IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["beekRepository.path.db4o"]), 0);
        private static readonly IBeekRepository beekRepos = new BeekRepository(beekServer);
        private static readonly IObjectServer userServer = Db4oFactory.OpenServer(IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["userRepository.path.db4o"]), 0);
        private static readonly IUserRepository userRepository = new UserRepository(userServer);

        public TestContext TestContext { get; set; }

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
            userRepository.UpdateUser(user);
            Assert.IsTrue(userRepository.GetUser(user.Id).Name.Equals(after));
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
        public void UserStaysInRoleAfterServerRestart()
        {
            string filePath = IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["userRepository.path.db4o"] + "1");
            IUser expected = GenerateTestUser();
            expected.AddRole(Roles.Anonymous);
            Assert.IsTrue(expected.Roles.Contains(Roles.Anonymous));
            IObjectServer userServer1 = Db4oFactory.OpenServer(filePath, 0);
            IUserRepository repo = new UserRepository(userServer1);
            repo.AddUser(expected);
            userServer1.Close();

            IObjectServer userServer2 = Db4oFactory.OpenServer(filePath, 0);
            IUserRepository repo2 = new UserRepository(userServer2);
            IUser actual = repo2.GetUser(expected.Id);
            Assert.IsTrue(actual.IsInRole(Roles.Anonymous));
        }

        private static IUser GenerateTestUser()
        {
            return new User("TestName", "TestPassword@123456", "test_test@test.com");
        }
    }
}
