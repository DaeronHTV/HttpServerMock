using System.Net.NetworkInformation;

namespace HttpServerMock.Core.Helper;

public class SocketHelper
{
    public static bool CheckPortAvailable(int port)
    {
        var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpConnectionInfo = ipGlobalProperties.GetActiveTcpListeners();
        return tcpConnectionInfo.All(endpoint => endpoint.Port != port);
    }
}