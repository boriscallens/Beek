using System;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Boris.BeekProject.Model.Tests
{
    [TestClass]
    public class Beeks
    {
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

        private static IEnumerable<IUser> GenerateWriters()
        {
            var users = GenerateUsers().ToList();
            foreach (IUser user in users)
            {
                user.AddRole(Roles.Writer);
            }
            return users;
        }
        private static IEnumerable<IUser> GenerateUsers()
        {
            for (int i = 0; i < 30; i++)
            {
                yield return new User("user" + i, "password", string.Empty){Id = Guid.NewGuid()};
            }
        }

        [TestMethod]
        public void CanInvolveUser()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            IUser writer = GenerateWriters().First();

            story.InvolveUser(writer, Roles.Writer);

            Assert.IsTrue(story.IsUserInvolvedAs(writer, Roles.Writer));
            // Duplicates should be ignored
            story.InvolveUser(writer, Roles.Writer);
            Assert.IsTrue(story.Involvements.Where(i => i.Key.Equals(writer) && i.Value.Equals(Roles.Writer)).Count() == 1);
        }

        [TestMethod]
        public void CanInvolveUsers()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            List<IUser> writers = GenerateWriters().ToList();

            story.InvolveUsers(writers, Roles.Writer);
            CollectionAssert.AreEquivalent(story.GetInvolvedUsersForRole(Roles.Writer).ToArray(), writers.ToArray());
            // Duplicates should be ignored
            story.InvolveUsers(writers, Roles.Writer);
            CollectionAssert.AreEquivalent(story.GetInvolvedUsersForRole(Roles.Writer).ToArray(), writers.ToArray());
        }

        [TestMethod]
        public void CanDisinvolveUser()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            IUser writer = GenerateWriters().First();

            // Removing without adding first should not throw an exception
            story.DisInvolveUser(writer, Roles.Writer);
            // Removing should remove the writer
            story.InvolveUser(writer, Roles.Writer);
            story.DisInvolveUser(writer, Roles.Writer);
            Assert.IsFalse(story.IsUserInvolvedAs(writer, Roles.Writer));
            // Removing inexisting writers should not throw an exception
            story.DisInvolveUser(writer, Roles.Writer);
            Assert.IsFalse(story.IsUserInvolvedAs(writer, Roles.Writer));
            // Removing a user from one role, should not remove him from his other roles
            writer.AddRole(Roles.Illustrator);
            story.InvolveUser(writer, Roles.Writer);
            story.InvolveUser(writer, Roles.Illustrator);
            story.DisInvolveUser(writer, Roles.Writer);
            Assert.IsTrue(story.IsUserInvolvedAs(writer, Roles.Illustrator));
            Assert.IsFalse(story.IsUserInvolvedAs(writer, Roles.Writer));
        }
   
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantAddNonWriterToWriters()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            IUser notWriter = GenerateUsers().First();
            Assert.IsFalse(notWriter.IsInRole(Roles.Writer));
            story.InvolveUser(notWriter, Roles.Writer);
        }
    }
}
