using System.Text;

namespace HttpServerMock.Core 
{ 
    public class BaseHttpObject
    {
        public IDictionary<string, string> Headers { get; set; }

        public string ContentType { get; set; }

        public Encoding Encoding { get; set; }
    }
}
