using HttpServerMock.Core;

namespace HttpServerMock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var handler = new Handler();
            handler.AddRoute(new BaseRoute());
            using (var server = new HttpMockServer(false, handler.HandleRequest))
            {
                server.Start();
                Console.WriteLine($"Adresse du serveur : {server.BaseUrl}");
                Console.ReadLine();
                server.Stop();
            }
        }
    }
}