using System;
using System.Collections.Generic;
using consoleJKO.Properties.Models;

namespace consoleJKO.Properties.Handler
{
    public class CmdGetTopCategory : CmdInterface
    {
        public CmdGetTopCategory(CmdInputStruct input)
        {
            cmdInput = input;
        }

        private CmdInputStruct cmdInput;

        private const string outputErrUnknownUser = "Error - unknown user";
        private const string outputSuccess = "Success";

        private string username { get; set; }


        protected Response ParseCmdOK()
        {
            Response resp = new Response(true, "");

            if (cmdInput.argvLst.Count != 2)
            {
                resp = new Response(false, "argvLst not valid");
                return resp;
            }

            username = cmdInput.argvLst[1];

            return resp;
        }

        protected Response CheckValid()
        {
            Response resp = new Response(true, "");
            TokenClass token = new TokenClass(username);

            User user = TokenClass.CheckAuth(token);

            if (user == null) // user not exist
            {
                resp = new Response(false, outputErrUnknownUser);

                return resp;
            }

            username = user.UserName;

            return resp;
        }

        protected Response ExecCmd()
        {
            Response resp;

            List<Category> ret = Category.GetTopCategory();

            string outputS = "";
            foreach(Category c in ret)
            {
                outputS += c.Name + "\n";
            }

            resp = new Response(true, outputS.Substring(0, outputS.Length - 1));
            return resp;
        }

        public Response HandleCmd()
        {
            Response resp;

            resp = ParseCmdOK();
            if (!resp.ok)
            {
                return resp;
            }

            resp = CheckValid();
            if (!resp.ok)
            {
                return resp;
            }


            resp = ExecCmd();

            return resp;
        }
    }
}
