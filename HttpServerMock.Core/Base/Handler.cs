using System.Net;
using System.Text;

namespace HttpServerMock.Core;

public class Handler : IHandler
{
    private readonly object _locker = new();
    private readonly TimeSpan _requestProcessingDelay = TimeSpan.Zero;
    private readonly IList<IRoute> _routes;

    public Handler()
    {
        _routes = new List<IRoute>();
        Encoding = Encoding.Default;
    }

    public IList<IRoute> Router => _routes;
    public Encoding Encoding { get; }

    public void AddRoute(IRoute route) => _routes.Add(route);

    public async void HandleRequest(HttpListenerContext context)
    {
        lock (_locker)
        {
            Task.Delay(_requestProcessingDelay).Wait();
        }
        try
        {
            using (var request = Request.ListenerToRequest(context.Request))
            {
                var route = Router.FirstOrDefault(route => request.IsTakeInChargeBy(route));
                if (route is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    var content = Encoding.GetBytes("<html><body>Mock Server: page not found</body></html>");
                    await context.Response.OutputStream.WriteAsync(content);
                }
                else
                {
                    var response = await route.GetResponse(request);
                    Response.ResponseToHttpListenerResponse(response, context.Response);
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            var content = Encoding.GetBytes($"{ConstHelper.ServerExceptionMessage} {ex.StackTrace}");
            await context.Response.OutputStream.WriteAsync(content);
        }
        finally
        {
            context.Response.Close();
        }
    }

    public void Dispose()
    {
    }
}
