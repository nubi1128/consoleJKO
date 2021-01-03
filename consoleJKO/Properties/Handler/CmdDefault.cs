using System;
namespace consoleJKO.Properties.Handler
{
    public class CmdDefault : CmdInterface
    {
        public CmdDefault(CmdInputStruct input)
        {
            
        }
        public Response HandleCmd()
        {
            Response resp = new Response(false, "Error - command not found");
            return resp;
        }
    }
}
