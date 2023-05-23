using System.Net;

namespace HttpServerMock.Core;

public sealed class Response: BaseHttpObject
{
    public Response(): base()
    {
        Headers = new Dictionary<string, string>();
    }

    public int StatusCode;

    public string Body { get; set; }

    public void AddHeader(string name, string value)
    {
        Headers.Add(name, value);
    }

    public static void ResponseToHttpListenerResponse(Response response, HttpListenerResponse result)
    {
        try
        {
            result.StatusCode = response.StatusCode;
            response.Headers.ToList().ForEach(pair => result.AddHeader(pair.Key, pair.Value));
            if (response.Body != null)
            {
                var content = System.Text.Encoding.UTF8.GetBytes(response.Body);
                result.OutputStream.Write(content, 0, content.Length);
            }
        }
        catch (Exception e)
        {
            result.StatusCode = (int)HttpStatusCode.OK;
            var content = System.Text.Encoding.UTF8.GetBytes($"{ConstHelper.ServerExceptionMessage} {e.StackTrace}");
            result.OutputStream.Write(content, 0, content.Length);
        }
    }
}
