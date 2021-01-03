using System;
using consoleJKO.Properties.Models;

namespace consoleJKO.Properties.Handler
{
    class TokenClass
    {
        public TokenClass(string usernameInput)
        {
            Username = usernameInput.ToLower();
        }
        public string Username { get; set; }

        public static User CheckAuth(TokenClass token)
        {
            User u = User.GetUserByUsername(token.Username);
            // todo handle error here or caller
            return u;
        }
    }
}