using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using consoleJKO.Properties.Handler;

namespace consoleJKO
{
    class MainClass
    {
        private const string CmdRegister = "REGISTER";
        private const string CmdCreateListing = "CREATE_LISTING";
        private const string CmdDeleteListing = "DELETE_LISTING";
        private const string CmdGetListing = "GET_LISTING";
        private const string CmdGetCategory = "GET_CATEGORY";
        private const string CmdGetTopCategory = "GET_TOP_CATEGORY";

        private static int Check(string username)
        {
            return -1;
        }


        private static void RequestBefore()
        {
            Console.Write("# ");
        }

        private static string GetRequest()
        {
            string line;
            line = Console.ReadLine();
            return line;
        }

        private static CmdInputStruct SplitRequest(string s)
        {
            string pattern = @"\w+|'[^']+'";
            Regex rgx = new Regex(pattern);

            List<string> argvLst = new List<string>();
            foreach (Match match in rgx.Matches(s))
            {
                var ss = match.Value;
                if (ss.Length > 0 && ss[0] == '\'')
                {
                    ss = ss.Substring(1, ss.Length - 2);
                }

                argvLst.Add(ss);
            }

            CmdInputStruct cmdInput = new CmdInputStruct(s, argvLst);

            return cmdInput;
        }

        private static void EachRequest()
        {
            RequestBefore();

            string inputS = GetRequest();

            CmdInputStruct cmdInput = SplitRequest(inputS);

            if (cmdInput.argvLst.Count <= 0)
            {
                return;
            }

            CmdInterface cmdI;
            switch (cmdInput.argvLst[0])
            {
                case CmdRegister:
                    cmdI = new CmdRegister(cmdInput);
                    break;

                case CmdCreateListing:
                    cmdI = new CmdCreateListing(cmdInput);
                    break;

                case CmdGetListing:
                    cmdI = new CmdGetListing(cmdInput);
                    break;

                case CmdGetCategory:
                    cmdI = new CmdGetCategory(cmdInput);
                    break;

                case CmdDeleteListing:
                    cmdI = new CmdDeleteListing(cmdInput);
                    break;

                case CmdGetTopCategory:
                    cmdI = new CmdGetTopCategory(cmdInput);
                    break;

                default:
                    cmdI = new CmdDefault(cmdInput);
                    break;

            }

            Response resp = cmdI.HandleCmd();
            Console.WriteLine("{0}", resp.resultStr);

        }

        public static void Main(string[] args)
        {
            while (true)
            {

                EachRequest();

            }

        }
    }
}
