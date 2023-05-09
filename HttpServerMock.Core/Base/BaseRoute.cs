namespace HttpServerMock.Core
{
    public sealed class BaseRoute : IRoute
    {
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

        public bool HandleRequest(Request request)
        {
            return request.Method == HttpMethod.Get && request.Body is null && string.IsNullOrEmpty(request.Query) && request.Url == "/";
        }
    }
}
