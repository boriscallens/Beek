using System;
using System.Linq;
using System.Collections.Generic;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Boris.BeekProject.Model.Tests
{
    [TestClass]
    public class Beeks
    {
        public TestContext TestContext { get; set; }
        private static readonly GenreFactory genreFactory = new GenreFactory(null);

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
                user.AddContribution(Contributions.Writer);
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
        
        // Relations to users
        [TestMethod]
        public void CanInvolveUser()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            IUser writer = GenerateWriters().First();

            story.InvolveUser(writer, Contributions.Writer);

            Assert.IsTrue(story.IsUserInvolvedAs(writer, Contributions.Writer));
            // Duplicates should be ignored
            story.InvolveUser(writer, Contributions.Writer);
            Assert.IsTrue(story.Involvements.Where(i => i.Key.Equals(writer) && i.Value.Equals(Contributions.Writer)).Count() == 1);
        }

        [TestMethod]
        public void CanInvolveUsers()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            List<IUser> writers = GenerateWriters().ToList();

            story.InvolveUsers(writers, Contributions.Writer);
            CollectionAssert.AreEquivalent(story.GetInvolvedUsersForContribution(Contributions.Writer).ToArray(), writers.ToArray());
            // Duplicates should be ignored
            story.InvolveUsers(writers, Contributions.Writer);
            CollectionAssert.AreEquivalent(story.GetInvolvedUsersForContribution(Contributions.Writer).ToArray(), writers.ToArray());
        }

        [TestMethod]
        public void CanDisinvolveUser()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            IUser writer = GenerateWriters().First();

            // Removing without adding first should not throw an exception
            story.DisInvolveUser(writer, Contributions.Writer);
            // Removing should remove the writer
            story.InvolveUser(writer, Contributions.Writer);
            story.DisInvolveUser(writer, Contributions.Writer);
            Assert.IsFalse(story.IsUserInvolvedAs(writer, Contributions.Writer));
            // Removing inexisting writers should not throw an exception
            story.DisInvolveUser(writer, Contributions.Writer);
            Assert.IsFalse(story.IsUserInvolvedAs(writer, Contributions.Writer));
            // Removing a user from one role, should not remove him from his other roles
            writer.AddContribution(Contributions.Illustrator);
            story.InvolveUser(writer, Contributions.Writer);
            story.InvolveUser(writer, Contributions.Illustrator);
            story.DisInvolveUser(writer, Contributions.Writer);
            Assert.IsTrue(story.IsUserInvolvedAs(writer, Contributions.Illustrator));
            Assert.IsFalse(story.IsUserInvolvedAs(writer, Contributions.Writer));
        }
   
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantAddNonWriterToWriters()
        {
            BaseBeek story = new BaseBeek(BeekTypes.ShortStory);
            IUser notWriter = GenerateUsers().First();
            Assert.IsFalse(notWriter.IsContributingAs(Contributions.Writer));
            story.InvolveUser(notWriter, Contributions.Writer);
        }

        [TestMethod]
        public void IsRelatedWorks()
        {
            BaseBeek original = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek copy = new BaseBeek(BeekTypes.ShortStory);
            copy.RelateTo(original, BeekRelationTypes.Original);
            Assert.IsTrue(copy.IsBeekRelatedToMeAs(original, BeekRelationTypes.Original));
        }

        // Relations to other beek
        [TestMethod]
        public void CanRelateExactlyOnceToOriginalPerType()
        {
            BaseBeek original = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek copy = new BaseBeek(BeekTypes.ShortStory);
            copy.RelateTo(original, BeekRelationTypes.Original);
            Assert.IsTrue(copy.Relations.Contains(new KeyValuePair<BaseBeek, BeekRelationTypes>(original, BeekRelationTypes.Original)));
            // Second time should be ignored as there is already one
            copy.RelateTo(original, BeekRelationTypes.Original);
            Assert.IsTrue(
                copy.Relations.Where(r=>r.Key == original && r.Value == BeekRelationTypes.Original).Count()==1);
        }

        [TestMethod]
        public void CanUnRelate()
        {
            BaseBeek original = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek copy = new BaseBeek(BeekTypes.ShortStory);
            copy.RelateTo(original, BeekRelationTypes.Original);
            copy.RelateTo(original, BeekRelationTypes.Original);
            Assert.IsTrue(copy.IsBeekRelatedToMeAs(original, BeekRelationTypes.Original));
            copy.UnrelateTo(original, BeekRelationTypes.Original);
            Assert.IsFalse(copy.IsBeekRelatedToMeAs(original, BeekRelationTypes.Original));
        }

        [TestMethod]
        public void GetRelatedBeek()
        {
            BaseBeek original = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek copy = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek copy2 = new BaseBeek(BeekTypes.ShortStory);
            original.RelateTo(copy, BeekRelationTypes.Republishment);
            original.RelateTo(copy2, BeekRelationTypes.Republishment);
            Assert.IsTrue(original.GetRelatedBeekForRelationType(BeekRelationTypes.Republishment).Count() == 2);
        }
    
        // Genres
        [TestMethod]
        public void CanGetGenreTree()
        {
            var genres = genreFactory.RebuildGenreTree();
            Assert.IsTrue(genres != null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantAddGenreToItself()
        {
            FantasyGenre genre = new FantasyGenre();
            genre.AddSubGenre(genre);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantAddParentGenreAsSubGenre()
        {
            FantasyGenre fantasy = new FantasyGenre();
            HistoricalFictionGenre history = new HistoricalFictionGenre();
            history.AddSubGenre(fantasy);
            fantasy.AddSubGenre(history);
        }

        [TestMethod]
        public void IsParentWorksThroughEntireBranch()
        {
            HistoricalFictionGenre history = new HistoricalFictionGenre();
            FantasyGenre fantasy = new FantasyGenre();
            CostumeDramaGenre costume = new CostumeDramaGenre();
            history.AddSubGenre(fantasy);
            fantasy.AddSubGenre(costume);
            Assert.IsTrue(history.IsParentOf(costume));
        }

        [TestMethod]
        public void IsChildWorksThroughEntireTree()
        {
            HistoricalFictionGenre history = new HistoricalFictionGenre();
            FantasyGenre fantasy = new FantasyGenre();
            CostumeDramaGenre costume = new CostumeDramaGenre();
            history.AddSubGenre(fantasy);
            fantasy.AddSubGenre(costume);
            Assert.IsTrue(costume.IsChildOf(history));
        }

        [TestMethod]
        public void CanAddGenreExactlyOnce()
        {
            BaseBeek beek = new BaseBeek(BeekTypes.ShortStory);
            FantasyGenre fantasy = new FantasyGenre();
            beek.AddGenre(fantasy);
            Assert.IsTrue(beek.Genres.Where(g => g.Equals(fantasy)).Count() == 1);
            // Should be ignored as it is already added
            beek.AddGenre(fantasy);
            Assert.IsTrue(beek.Genres.Where(g => g.Equals(fantasy)).Count() == 1);
        }

        [TestMethod]
        public void IsGenreDetectsGenre()
        {
            BaseBeek beek = new BaseBeek(BeekTypes.ShortStory);
            var autoBiography = new AutoBiographyGenre();
            beek.AddGenre(autoBiography);
            Assert.IsTrue(beek.IsGenre(autoBiography));
        }
        
        [TestMethod]
        public void IsGenreDetectsChildGenre()
        {
            BaseBeek beek = new BaseBeek(BeekTypes.ShortStory);
            var biography = new BiographyGenre();
            var autoBiography = new AutoBiographyGenre();
            biography.AddSubGenre(autoBiography);
            beek.AddGenre(autoBiography);
            Assert.IsTrue(beek.IsGenre(biography));
        }
    
        [TestMethod]
        public void CanRemoveGenre()
        {
            BaseBeek beek = new BaseBeek(BeekTypes.ShortStory);
            var biography = new BiographyGenre();
            var autoBiography = new AutoBiographyGenre();
            biography.AddSubGenre(autoBiography);
            beek.AddGenre(autoBiography);
            Assert.IsTrue(beek.IsGenre(autoBiography));
            beek.RemoveGenre(autoBiography);
            Assert.IsFalse(beek.IsGenre(autoBiography));
        }

        // Volumes logic
        [TestMethod]
        public void CanAddToCollection()
        {
            BaseBeek vol1 = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek vol2 = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek vol3 = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek vol3Bis = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek vol3B = new BaseBeek(BeekTypes.ShortStory);

            BeekCollection collection = new BeekCollection();
            vol1.AddToCollection(collection, 1, null);
            vol2.AddToCollection(collection, 2, null);
            vol3.AddToCollection(collection, 3, null);
            Assert.IsTrue(collection.Count == 3);
            Assert.IsTrue(vol1.Collection == collection);
            Assert.IsTrue(vol1.VolumeNumber == 1);
            Assert.IsTrue(vol1.TotalVolumes == 3);

            vol3Bis.AddToCollection(collection, 3, null);
            Assert.IsTrue(vol1.TotalVolumes == 3);

            vol3B.AddToCollection(collection, 3, 'B');
            Assert.IsTrue(vol1.TotalVolumes == 4);
        }

        [TestMethod]
        public void CanRemoveFromCollection()
        {
            BaseBeek vol1 = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek vol2a = new BaseBeek(BeekTypes.ShortStory);
            BaseBeek vol2b = new BaseBeek(BeekTypes.ShortStory);
            BeekCollection collection = new BeekCollection();
            vol1.AddToCollection(collection, 1, null);
            vol2a.AddToCollection(collection, 2, 'a');
            vol2b.AddToCollection(collection, 2, 'b');
            Assert.IsTrue(vol1.Collection.Contains(vol2b));
            vol2b.RemoveFromCollection();
            Assert.IsFalse(vol1.Collection.Contains(vol2b));
            Assert.IsTrue(vol2b.VolumeNumber == 0);
        }
    
    }
}
