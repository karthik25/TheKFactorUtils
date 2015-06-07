using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using Moq;

namespace TheKFactorUtils.Mocks
{
    public static class HttpContextMocks
    {
        public static HttpContextBase GetMockContext(bool isAuthenticated, string url = null, IIdentity identity = null)
        {
            var context = new Mock<HttpContextBase>();

            context.SetupGet(c => c.Request).Returns(GetMockRequest(context.Object, isAuthenticated, url));
            context.SetupGet(c => c.Response).Returns(GetMockResponse(null));
            context.SetupGet(c => c.IsDebuggingEnabled).Returns(true);

            if (identity != null)
            {
                IPrincipal principal = new GenericPrincipal(identity, null);
                context.SetupGet(c => c.User).Returns(principal);
            }

            return context.Object;
        }

        public static HttpRequestBase GetMockRequest(HttpContextBase httpContextBase, bool isAuthenticated,
                                                       string requestUrl = null)
        {
            var reqUrl = requestUrl ?? "http://localhost";
            var uri = new Uri(reqUrl);
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(p => p.Url).Returns(uri);
            request.SetupGet(p => p.AppRelativeCurrentExecutionFilePath).Returns(requestUrl);
            request.SetupGet(p => p.IsAuthenticated).Returns(isAuthenticated);
            request.SetupGet(p => p.RequestContext).Returns(httpContextBase.GetMockRequestContext());
            request.SetupGet(p => p.ApplicationPath).Returns(@"/");
            request.SetupGet(p => p.PathInfo).Returns(string.Empty);
            request.SetupGet(p => p.ServerVariables).Returns(new NameValueCollection());
            return request.Object;
        }

        public static RequestContext GetMockRequestContext(this HttpContextBase httpContextBase)
        {
            var requestContext = new Mock<RequestContext>();
            requestContext.SetupProperty(r => r.HttpContext, httpContextBase);
            requestContext.SetupProperty(r => r.RouteData, new RouteData());
            return requestContext.Object;
        }

        public static HttpResponseBase GetMockResponse(string virtualPath)
        {
            var response = new Mock<HttpResponseBase>();
            response.Setup(x => x.ApplyAppPathModifier(virtualPath)).Returns(virtualPath);
            return response.Object;
        }

        public static HttpContext GetFakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
    }
}