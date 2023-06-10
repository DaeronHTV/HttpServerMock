using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace HttpServerMock.Core.Helper;

/// <summary>
/// 
/// </summary>
public class CertificateHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="certificate"></param>
    /// <param name="storeName"></param>
    /// <param name="storeLocation"></param>
    /// <returns></returns>
    public static bool IsCertificateRegistered(X509Certificate2 certificate, StoreName storeName,
        StoreLocation storeLocation)
    {
        try
        {
            if (certificate is not null)
            {
                X509Store store = new(storeName, storeLocation);
                store.Open(OpenFlags.ReadWrite);
                return store.Certificates.Contains(certificate);
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sslCertificate"></param>
    /// <param name="port"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static bool TryBindSslCertificate(X509Certificate2 sslCertificate, int port, int timeout = 1000)
    {
        var osInfo = Environment.OSVersion;
        try
        {
            var appId = Guid.NewGuid();
            string[] arguments = osInfo.Platform switch
            {
                PlatformID.Unix =>
                    new[] {$"http add sslcert ipport=0.0.0.0:{port} certhash={sslCertificate.Thumbprint} appid={{{appId}}}"}, //TODO
                PlatformID.MacOSX =>
                    new[] {$"http add sslcert ipport=0.0.0.0:{port} certhash={sslCertificate.Thumbprint} appid={{{appId}}}"}, //TODO
                _ => new[] {"netsh", $"http add sslcert ipport=0.0.0.0:{port} certhash={sslCertificate.Thumbprint} appid={{{appId}}}"}
            };
            ProcessStartInfo processInfo = new(arguments[0], arguments[1])
            {
                RedirectStandardOutput = true,
                UseShellExecute = true,
                CreateNoWindow = true
            };
            using (Process process = new())
            {
                process.StartInfo = processInfo;
                process.Start();
                if (!process.WaitForExit(timeout))
                {
                    Console.WriteLine("Certificate - Timeout occured during binding the port with the certificate");
                    return false;
                }
                return true;
            }
        }
        catch
        {
            Console.WriteLine("Certificate - An error occured during the binding of the port with the ssl certificate");
            return false;
        }
    }
}