using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerMock.Core.Fluent
{
    public partial class RouteBuilder
    {
        private string ContentType;

        public RouteBuilder WithBody(string body)
        {
            Response.Body = body;
            return this;
        }

        public RouteBuilder WithBody(string body, string ContentType)
        {
            return this;
        }

        public RouteBuilder WithBodyAsJson(string body)
        {

        }

        public RouteBuilder WithBodyAsJson(object body)
        {

        }
    }
}
