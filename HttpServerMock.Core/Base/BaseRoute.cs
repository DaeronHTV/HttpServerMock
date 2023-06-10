namespace HttpServerMock.Core;

/// <summary>
/// A basic route that tells if the server is running or not
/// </summary>
public sealed class BaseRoute : IRoute
{
    private const string TextPlain = "text/plain";
    private const string Response = "Server is running !";
    
    /// <inheritdoc/>
    public async Task<Response> GetResponse(Request request)
    {
        return await Task.Run(() => new Response()
        {
            StatusCode = 200,   
            Body = Response,
            ContentType = TextPlain,
        });
    }

    /// <inheritdoc/>
    public bool HandleRequest(Request request)
    {
        return request.Method == HttpMethod.Get && request.Body is null && string.IsNullOrEmpty(request.Query) && request.Url == "/";
    }
}
