using System;
using System.Collections.Generic;
using consoleJKO.Properties.Models;

namespace consoleJKO.Properties.Handler
{
    public class CmdGetCategory : CmdInterface
    {
        public CmdGetCategory(CmdInputStruct input)
        {
            cmdInput = input;
        }

        private CmdInputStruct cmdInput;

        private const string outputErrUnknownUser = "Error - unknown user";
        private const string outputErrCategoryNotFound = "Error - category not found";
        private const string outputSuccess = "Success";

        private string username { get; set; }
        private string category { get; set; }
        private string sortBy { get; set; }
        private bool isAsc { get; set; }
      
        protected Response ParseCmdOK()
        {
            Response resp = new Response(true, "");

            if (cmdInput.argvLst.Count != 5)
            {
                resp = new Response(false, "argvLst not valid");
                return resp;
            }

            username = cmdInput.argvLst[1];
            category = cmdInput.argvLst[2];
            sortBy = cmdInput.argvLst[3];

            isAsc = true;
            if ( cmdInput.argvLst[4] == "dsc" )
            {
                isAsc = false;
            }
            
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

            List<Listing> ret = Listing.GetListingByCategory(category, sortBy, isAsc);
            if (ret.Count == 0)
            {
                resp = new Response(false, outputErrCategoryNotFound);
                return resp;
            }
            string outputS = "";
            foreach ( Listing item in ret)
            {
                outputS += item.ToString() + "\n";
            }
            
            resp = new Response(true, outputS.Substring(0, outputS.Length-1));

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
