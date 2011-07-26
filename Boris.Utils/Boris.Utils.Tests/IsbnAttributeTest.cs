using Boris.Utils.Mvc.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    /// <summary>
    ///This is a test class for IsbnAttributeTest and is intended
    ///to contain all IsbnAttributeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IsbnAttributeTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void Isbn10IsValid()
        {
            var target = new IsbnAttribute();
            object value = "0552153370";
            var actual = target.IsValid(value);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void Isbn13IsValid()
        {
            var target = new IsbnAttribute();
            object value = "ISBN 978-90-443-1614-8";
            var actual = target.IsValid(value);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void Isbn10WithXIsValid()
        {
            var target = new IsbnAttribute();
            object value = "055215329X";
            var actual = target.IsValid(value);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void Isbn10WithInvalidCheckIsFalse()
        {
            var target = new IsbnAttribute();
            object value = "0552153251";
            var actual = target.IsValid(value);
            Assert.AreEqual(false, actual);
        }
    }
}
