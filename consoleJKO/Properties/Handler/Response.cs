using System;
namespace consoleJKO.Properties.Handler
{
    public class Response
    {
        public bool ok { get; set; }
        public string resultStr { get; set; }

        public Response(bool inputOk, string inputResultStr)
        {
            ok = inputOk;
            resultStr = inputResultStr;
        }
    }
}
