namespace HttpServerMock.Core
{
    public interface IRoute
    {
        bool HandleRequest(Request request);

        Task<Response> GetResponse(Request request);
    }
}
