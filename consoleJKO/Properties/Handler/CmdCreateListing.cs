using System;
using consoleJKO.Properties.Models;

namespace consoleJKO.Properties.Handler
{
    public class CmdCreateListing : CmdInterface
    {
        public CmdCreateListing(CmdInputStruct input)
        {
            cmdInput = input;
        }

        private CmdInputStruct cmdInput;

        private const string outputErrUnknownUser = "Error - unknown user";
        private const string outputSuccess = "Success";

        private string username { get; set; }
        private string itemName { get; set; }
        private string itemContent { get; set; }
        private int itemPrice { get; set; }
        private string itemCategory { get; set; }

        private int userID { get; set; }


        protected Response ParseCmdOK()
        {
            Response resp = new Response(true, "");

            if (cmdInput.argvLst.Count != 6)
            {
                resp = new Response(false, "argvLst not valid");
                return resp;
            }

            username = cmdInput.argvLst[1];
            itemName = cmdInput.argvLst[2];
            itemContent = cmdInput.argvLst[3];
            itemPrice = Int32.Parse(cmdInput.argvLst[4]);
            itemCategory = cmdInput.argvLst[5];

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
            userID = user.ID;

            return resp;
        }

        protected Response ExecCmd()
        {
            Response resp;

            CreateListingInput createInput = new CreateListingInput();
            createInput.Username = username;
            createInput.ItemName = itemName;
            createInput.ItemContent = itemContent;
            createInput.ItemPrice = itemPrice;
            createInput.CategoryName = itemCategory;

            Listing item = Listing.CreateListing(createInput);
            if (item == null)
            {
                // todo error
                resp = new Response(false, "");
            }
            else
            {
                string idS = string.Format("{0}", item.ID);
                resp = new Response(true, idS);
            }

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
