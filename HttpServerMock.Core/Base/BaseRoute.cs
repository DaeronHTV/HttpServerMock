namespace HttpServerMock.Core;

/// <summary>
/// A basic route that tells if the server is running or not
/// </summary>
public sealed class BaseRoute : IRoute
{
    /// <inheritdoc/>
    public async Task<Response> GetResponse(Request request)
    {
        var response = new Response()
        {
            StatusCode = 200,   
            Body = "Server is running !",
            ContentType = "text/plain",
        };
        return response;
    }

    /// <inheritdoc/>
    public bool HandleRequest(Request request)
    {
        return request.Method == HttpMethod.Get && request.Body is null && string.IsNullOrEmpty(request.Query) && request.Url == "/";
    }
}
