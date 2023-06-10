using System.Net;
using System.Text;

namespace HttpServerMock.Core
{
    public interface IHandler: IDisposable
    {
        IList<IRoute> Router { get; }
        
        Encoding Encoding { get; }
        
        void HandleRequest(HttpListenerContext context);
        
        void AddRoute(IRoute route);
    }
}
