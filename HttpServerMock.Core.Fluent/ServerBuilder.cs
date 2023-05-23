using JetBrains.Annotations;

namespace HttpServerMock.Core.Fluent;

/// <summary>
/// 
/// </summary>
public class ServerBuilder
{
    private bool isSsl;
    private int port;
    private IHandler handler;

    public ServerBuilder()
    {
        handler = new Handler();
    }

    /// <summary>
    /// Create an instance of the builder
    /// </summary>
    /// <returns>The builder</returns>
    [PublicAPI]
    public static ServerBuilder Create() => new();

    /// <summary>
    /// Create the HttpServer mocked and launch it if the user want
    /// </summary>
    /// <param name="launch">Tell if we start the mocked server or not</param>
    /// <returns>The mocked server</returns>
    public HttpMockServer Start(bool launch = false)
    {
        var server = new HttpMockServer(isSsl, handler.HandleRequest, port);
        if (launch)
        {
            server.Start();
        }
        return server;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The builder</returns>
    public ServerBuilder WithSsl()
    {
        isSsl = true;
        return this;
    }

    /// <summary>
    /// Indicates the port to use for the server. If the port isn't accesible (already used)
    /// the port will be automatically changed
    /// </summary>
    /// <param name="port">The port to use for the server</param>
    /// <returns>The builder</returns>
    public ServerBuilder UsingPort(int port)
    {
        this.port = port;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="route"></param>
    /// <returns>The builder</returns>
    public ServerBuilder AddRoute(IRoute route)
    {
        handler.AddRoute(route);
        return this;
    }
}
