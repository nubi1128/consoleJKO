using System;
using System.Collections.Generic;

namespace consoleJKO.Properties.Models
{
    public class PutItemInput<T> where T : class
    {
        public PutItemInput()
        {
            
            
        }

        private int PK { get; set; }
        public string key { get; set; }
        public string tableName { get; set; }
        public T data  { get; set; }
    }

    public class PutItemOutput<T> where T : class
    {
        public PutItemOutput()
        {

        }

        private int PK { get; set; }
        public string key { get; set; }
        public string tableName { get; set; }
        public T data { get; set; }
    }

}

