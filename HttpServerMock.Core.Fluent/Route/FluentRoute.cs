using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerMock.Core.Fluent;

public class FluentRoute : IRoute
{
    internal HttpMethod Method { get; set; }

    public Task<Response> GetResponse(Request request)
    {
        throw new NotImplementedException();
    }

    public bool HandleRequest(Request request)
    {
        throw new NotImplementedException();
    }
}
