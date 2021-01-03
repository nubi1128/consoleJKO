using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace consoleJKO.Properties.Models
{
    public class User
    {
        public int ID { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }

        private static int count;
        private static int getID()
        {
            count = count + 1;
            return count;
        }


        public static User CreateUser(string usernameInput)
        {
            string username = usernameInput.ToLower();
            if (StorageImpl.QueryUser(username) != null)
            {
                return null; // todo error
            }

            User u = new User
            {
                UserName = username,
                ID = getID()
            };
            StorageImpl.CreateUser(u);

            return u;
        }

        public static User GetUserByUsername(string usernameInput)
        {
            string username = usernameInput.ToLower();
            User dbItem = StorageImpl.QueryUser(username);

            if (dbItem != null)
            {
                return dbItem;
            }

            return null;
        }
    }
}
