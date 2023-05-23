using System.Collections.Specialized;
using System.Net;

namespace HttpServerMock.Core;

public sealed class Request: BaseHttpObject, IDisposable
{
    private bool isDisposed = false;

    public string Url { get; set; }

    public string Query { get; set; }

    public HttpMethod Method { get; set; }

    public Stream Body { get; set; }

    public static Request ListenerToRequest(HttpListenerRequest request)
    {
        return new Request
        {
            Url = request.Url.AbsolutePath,
            Query = request.Url.Query,
            Method = new HttpMethod(request.HttpMethod),
            ContentType = request.ContentType ?? "text/plain",
            Encoding = request.ContentEncoding,
            Headers = HeadersToDictionary(request.Headers),
            Body = GetRequestBody(request)
        };
    }

    private static Stream GetRequestBody(HttpListenerRequest request)
    {
        if (!request.HasEntityBody)
        {
            return null!;
        }
        return request.InputStream;
    }

    private static IDictionary<string, string> HeadersToDictionary(NameValueCollection headers)
    {
        var dico = new Dictionary<string, string>();
        foreach (var kv in headers.AllKeys)
        {
            dico[kv!] = headers[kv]!;
        }
        return dico;
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            Body?.Dispose();
        }
    }
}

public static class RequestExtension
{
    public static bool IsTakeInChargeBy(this Request request, IRoute route)
    {
        return route.HandleRequest(request);
    }
}
