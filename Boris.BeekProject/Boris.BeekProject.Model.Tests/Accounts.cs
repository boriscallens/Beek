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

            Assert.IsFalse(user.IsContributingAs(Contributions.Writer));
            user.AddContribution(Contributions.Writer);
            Assert.IsTrue(user.IsContributingAs(Contributions.Writer));
            user.AddContribution(Contributions.Writer);
            Assert.IsTrue(user.Contributions.Where(r => r.Equals(Contributions.Writer)).Count() == 1);
            user.AddContribution(Contributions.Illustrator);
            Assert.IsTrue(user.IsContributingAs(Contributions.Writer) && user.IsContributingAs(Contributions.Illustrator));
        }

        [TestMethod]
        public void IsInRoleShouldBeTrueWhenInThatRole()
        {
            IUser user = new User();
            user.AddContribution(Contributions.Writer);

            Assert.IsTrue(user.IsContributingAs(Contributions.Writer));
            Assert.IsTrue(user.IsInRole(Contributions.Writer.ToString()));
            Assert.IsFalse(user.IsContributingAs(Contributions.Illustrator));
            Assert.IsFalse(user.IsInRole(Contributions.Illustrator.ToString()));
        }
    }
}