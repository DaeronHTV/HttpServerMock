using System.Net;
using System.Text;

namespace HttpServerMock.Core
{
    public class Handler : IHandler
    {
        private readonly object locker = new();
        private readonly TimeSpan _requestProcessingDelay = TimeSpan.Zero;
        protected IList<IRoute> routes;

        public Handler() 
        { 
            routes = new List<IRoute>();
        }

        public IList<IRoute> Router => routes;

        public void AddRoute(IRoute route) => routes.Add(route);

        public async void HandleRequest(HttpListenerContext context)
        {
            lock (locker)
            {
                Task.Delay(_requestProcessingDelay).Wait();
            }
            try
            {
                var request = Request.ListenerToRequest(context.Request);
                var route = Router.FirstOrDefault(route => request.IsTakeInChargeBy(route));
                if (route is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    var content = Encoding.UTF8.GetBytes("<html><body>Mock Server: page not found</body></html>");
                    context.Response.OutputStream.Write(content, 0, content.Length);
                }
                else
                {
                    var response = await route.GetResponse(request);
                    Response.ResponseToHttpListenerResponse(response, context.Response);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                var content = Encoding.UTF8.GetBytes($"{ConstHelper.ServerExceptionMessage} {ex.StackTrace}");
                context.Response.OutputStream.Write(content, 0, content.Length);
            }
            finally
            {
                context.Response.Close();
            }
        }
    }
}
