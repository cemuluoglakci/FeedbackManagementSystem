using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

using static System.Convert;

namespace ApplicationFMS.Helpers
{
    public static class Security
    {
        public static string GenerateSalt()
        {
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] saltBytes = new byte[16];
            randomNumberGenerator.GetBytes(saltBytes);
            string saltText = ToBase64String(saltBytes);
            return saltText;
        }

        public static string SaltAndHashPassword(string password, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string saltedPassword = password + salt;
                return ToBase64String(sha.ComputeHash(
                  Encoding.Unicode.GetBytes(saltedPassword)));
            }
        }

        public static bool CheckPassword(string password, string salt, string hashedPassword)
        {
            string saltedhashedPassword = SaltAndHashPassword(password, salt);

            return (saltedhashedPassword == hashedPassword);
        }




        public static void LogIn(string username, string password)
        {
            if (CheckPassword(username, password, ""))
            {
                GenericIdentity gi = new(
                  name: username, type: "");

                GenericPrincipal gp = new(
                  identity: gi, roles: null //User[username].Roles
                  );

                // set the principal on the current thread so that
                // it will be used for authorization by default
                Thread.CurrentPrincipal = gp;
            }
        }
    }
}
