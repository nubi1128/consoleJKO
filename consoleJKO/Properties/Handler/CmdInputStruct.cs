using System;
using System.Collections.Generic;

namespace consoleJKO.Properties.Handler
{
    public struct CmdInputStruct
    {
        public CmdInputStruct(string s, List<string> input)
        {
            argvLst = input;
            cmdInputS = s;
        }
        public List<string> argvLst { get; set; }
        public string cmdInputS { get; set; }
    }

}
