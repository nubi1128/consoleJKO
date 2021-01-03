using System;
using System.Collections.Generic;
using consoleJKO.Properties.Models;

namespace consoleJKO.Properties.Handler
{
    public class CmdRegister : CmdInterface
    {
        public CmdRegister(CmdInputStruct input)
        {
            cmdInput = input;
        }

        private CmdInputStruct cmdInput;

        private const string outputErrUserExist = "Error - user already existing";
        private const string outputSuccess = "Success";

        //private string username { get; set; }
        private int userID { get; set; }

        protected Response ParseCmdOK()
        {
            Response resp = new Response(true, "");
            if (cmdInput.argvLst.Count != 2)
            {
                resp = new Response(false, "argvLst not valid");
            }

            return resp;
        }
       

        protected Response ExecCmd()
        {
            Response resp = new Response(true, outputSuccess);

            User user = User.CreateUser(cmdInput.argvLst[1]);
            if (user == null)
            {
                // todo error
                //Console.WriteLine(outputErrUserExist);
                resp = new Response(false, outputErrUserExist);
            }
            else
            {
                //Console.WriteLine("{0}: {1}", outputSuccess, user.ID);
            }

            return resp;
        }

        public Response HandleCmd()
        {
            Response resp;

            resp = ParseCmdOK();
            if (!resp.ok)
            {
                //Console.WriteLine( "parse cmd ok failed");
                return resp;
            }

            resp = ExecCmd();
            return resp;
        }
    }
}
