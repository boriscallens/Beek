using System.Linq;
using Boris.BeekProject.Model.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Boris.BeekProject.Model.Tests
{
    [TestClass]
    public class Accounts
    {
        public TestContext TestContext { get; set; }

        public Accounts(){}

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
        public void AddAndRemoveRoleShouldAddAndRemoveRolesWithoutDuplicates()
        {
            IUser user = new User();

            Assert.IsFalse(user.IsInRole(Roles.Writer));
            user.AddRole(Roles.Writer);
            Assert.IsTrue(user.IsInRole(Roles.Writer));
            user.AddRole(Roles.Writer);
            Assert.IsTrue(user.Roles.Where(r => r.Equals(Roles.Writer)).Count() == 1);
            user.AddRole(Roles.Illustrator);
            Assert.IsTrue(user.IsInRole(Roles.Writer) && user.IsInRole(Roles.Illustrator));
        }

        [TestMethod]
        public void IsInRoleShouldBeTrueWhenInThatRole()
        {
            IUser user = new User();
            user.AddRole(Roles.Writer);

            Assert.IsTrue(user.IsInRole(Roles.Writer));
            Assert.IsTrue(user.IsInRole(Roles.Writer.ToString()));
            Assert.IsFalse(user.IsInRole(Roles.Illustrator));
            Assert.IsFalse(user.IsInRole(Roles.Illustrator.ToString()));
        }
    }
}