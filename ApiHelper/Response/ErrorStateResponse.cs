using System.Collections.Generic;

namespace ApiHelper.Response
{
    public class ErrorStateResponse
    {
        public IDictionary<string, string[]> ModelState { get; set; }
    }
}
