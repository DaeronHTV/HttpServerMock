using JetBrains.Annotations;

namespace HttpServerMock.Core.Fluent;

/// <summary>
/// 
/// </summary>
public partial class RouteBuilder
{
    private delegate bool handleRequestDelegate(Request request);
    private delegate Task<Response> getResponseDelegate(Request request);

    #region OPTIONS
    private IDictionary<string, Type> queryParameters;
    private string requestContentType;
    private string responseContentType;
    private HttpMethod method;
    #endregion

    public RouteBuilder() 
    { 
        requestContentType = string.Empty;
        queryParameters = null!;
    }

    [PublicAPI]
    public static RouteBuilder Create() => new();

    internal IRoute GenerateRoute()
    {

        return default!;
    }

}
