using System.Net;

namespace HttpServerMock.Core
{
    public interface IHandler
    {
        void HandleRequest(HttpListenerContext context);

        IList<IRoute> Router { get; }

        void AddRoute(IRoute route);
    }
}
