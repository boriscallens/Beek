using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using Boris.BeekProject.Guis.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Boris.BeekProject.Guis.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
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
        public void RouteSearchBeek()
        {
            //Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/Search/Beek");
            var routes = new RouteCollection();
            routes = MvcRouteFactory.GetRoutes(routes, false);

            // Act 
            RouteData routeData = routes.GetRouteData(context);

            // Assert 
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Search", routeData.Values["controller"]);
            Assert.AreEqual("Beek", routeData.Values["action"]);
            //Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]); 
        }
    }

    public class StubHttpContextForRouting : HttpContextBase
    {
        readonly StubHttpRequestForRouting request;
        readonly StubHttpResponseForRouting response;

        public StubHttpContextForRouting(string appPath = "/", string requestUrl = "~/")
        {
            request = new StubHttpRequestForRouting(appPath, requestUrl);
            response = new StubHttpResponseForRouting();
        }
        public override HttpRequestBase Request
        {
            get { return request; }
        }
        public override HttpResponseBase Response
        {
            get { return response; }
        }
    }
    public class StubHttpRequestForRouting : HttpRequestBase
    {
        readonly string appPath;
        readonly string requestUrl;

        public StubHttpRequestForRouting(string appPath, string requestUrl)
        {
            this.appPath = appPath;
            this.requestUrl = requestUrl;
        }

        public override string ApplicationPath
        {
            get { return appPath; }
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get { return requestUrl; }
        }

        public override string PathInfo
        {
            get { return ""; }
        }

        public override NameValueCollection ServerVariables
        {
            get { return new NameValueCollection(); }
        }
    }
    public class StubHttpResponseForRouting : HttpResponseBase
    {
        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}
