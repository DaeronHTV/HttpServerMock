﻿using System.Net;
using System.Net.Sockets;

namespace HttpServerMock.Core;

/// <summary>
/// 
/// </summary>
public class HttpMockServer : IDisposable
{
    #region informations sur le serveur
    private readonly string baseUrl;
    private readonly int port;
    #endregion
    #region Gestion des appels http
    private readonly Action<HttpListenerContext> _httpHandler;
    private readonly HttpListener _listener;
    private CancellationTokenSource _cts;
    private Task httpTask;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isSsl"></param>
    /// <param name="httpHandler"></param>
    /// <param name="port"></param>
    public HttpMockServer(bool isSsl, Action<HttpListenerContext> httpHandler, int port = 0)
    {
        if (port == 0)
            port = FindFreePort();
        this.port = port;
        this._httpHandler = httpHandler;
        var protocol = isSsl ? "https" : "http";
        baseUrl = $"{protocol}://localhost:{port}/";
        _listener = new HttpListener();
        _listener.Prefixes.Add(baseUrl);
    }

    /// <summary>
    /// 
    /// </summary>
    public string BaseUrl { get => baseUrl; }

    /// <summary>
    /// 
    /// </summary>
    public int Port { get => port; }

    /// <summary>
    /// 
    /// </summary>
    public void Start()
    {
        _listener.Start();
        _cts = new CancellationTokenSource();
        httpTask = Task.Run(async () =>
        {
            using (_listener)
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    HttpListenerContext context = await _listener.GetContextAsync();
                    _httpHandler(context);
                }
            }
        }
        , _cts.Token);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Stop()
    {
        _cts.Cancel();
        _listener.Stop();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _cts?.Dispose();
        _listener?.Close();
        while(!httpTask.IsCompleted){ }
        httpTask?.Dispose();
    }

    #region PRIVATE METHODS
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int FindFreePort()
    {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
    #endregion
}
