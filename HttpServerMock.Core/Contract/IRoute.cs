namespace HttpServerMock.Core;

/// <summary>
/// Contract that describes the methods to create a route in the HTTPServer
/// </summary>
public interface IRoute
{
    /// <summary>
    /// Indicates if the route can handle the request did by the client
    /// </summary>
    /// <param name="request">The request of the client</param>
    /// <returns>True if the route can handle the request, False else</returns>
    bool HandleRequest(Request request);

    /// <summary>
    /// Return a response for the request made by the client
    /// </summary>
    /// <param name="request">The http request of the client</param>
    /// <returns>A Request object containing data of the http request made by the client</returns>
    Task<Response> GetResponse(Request request);
}
