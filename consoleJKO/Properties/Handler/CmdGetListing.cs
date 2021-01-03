using System;
using consoleJKO.Properties.Models;

namespace consoleJKO.Properties.Handler
{
    public class CmdGetListing : CmdInterface
    {
        public CmdGetListing(CmdInputStruct input)
        {
            cmdInput = input;
        }

        private CmdInputStruct cmdInput;

        private const string outputErrUnknownUser = "Error - unknown user";
        private const string outputErrNotFound = "Error - not found";
        private const string outputSuccess = "Success";

        private string username { get; set; }
        private int itemID { get; set; }

        protected Response ParseCmdOK()
        {
            Response resp = new Response(true, "");

            if (cmdInput.argvLst.Count != 3)
            {
                resp = new Response(false, "argvLst not valid");
                return resp;
            }

            username = cmdInput.argvLst[1];

            try
            {
                itemID = Int32.Parse(cmdInput.argvLst[2]);
            }
            catch (FormatException e)
            {
                resp = new Response(false, e.Message);
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

            Listing item = Listing.GetListingByID(itemID);
            if (item == null)
            {
                resp = new Response(false, outputErrNotFound);
                return resp;
            }

            string outputS = item.ToString();

            resp = new Response(true, outputS);

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
