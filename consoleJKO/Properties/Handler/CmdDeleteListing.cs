using System;
using consoleJKO.Properties.Models;

namespace consoleJKO.Properties.Handler
{
    public class CmdDeleteListing : CmdInterface
    {
        public CmdDeleteListing(CmdInputStruct input)
        {
            cmdInput = input;
        }

        private CmdInputStruct cmdInput;

        private const string outputErrUnknownUser = "Error - unknown user";
        private const string outputErrListingNotExist = "Error - listing does not exist";
        private const string outputErrListingOwnerNotMatch = "Error - listing owner not match";
        private const string outputSuccess = "Success";

        private string username { get; set; }
        private int itemID { get; set; }

        private int userID { get; set; }


        protected Response ParseCmdOK()
        {
            Response resp = new Response(true, "");

            if (cmdInput.argvLst.Count != 3)
            {
                resp = new Response(false, "argvLst not valid");
                return resp;
            }

            username = cmdInput.argvLst[1];
            itemID = Int32.Parse(cmdInput.argvLst[2]);

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

            Listing target = Listing.GetListingByID(itemID);
            if (target == null)
            {
                resp = new Response(false, outputErrListingNotExist);
                return resp;
            }

            if (target.Username != username)
            {
                resp = new Response(false, outputErrListingOwnerNotMatch);
                return resp;
            }

            if (!Listing.DeleteListing(itemID))
            {
                resp = new Response(false, outputErrListingNotExist);
                return resp;
            }

            resp = new Response(true, outputSuccess);
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
