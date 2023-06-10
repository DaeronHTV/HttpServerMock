using System.Net;
using System.Text;

namespace HttpServerMock.Core.Fluent;

internal class FluentHandler : IHandler
{
    public IList<IRoute> Router => throw new NotImplementedException();
    public Encoding Encoding { get; }

    public void AddRoute(IRoute route)
    {
        throw new NotImplementedException();
    }

    public void HandleRequest(HttpListenerContext context)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}
