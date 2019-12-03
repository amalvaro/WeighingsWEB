using System;
using Database;
using System.Linq;
using System.Text;
using Entities.Entities;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace Authentication
{

    public class AuthenticationManager
    {
        private UserContext usrContext  { get; set; }
        private string usrLogin         { get; set; }
        private string usrPswd          { get; set; }

        public AuthenticationManager(UserContext context, string login, string password) {
            this.usrLogin = login;
            this.usrPswd  = password;
            this.usrContext = context;
        }

        public bool TryAuthorize() {

            Users user = usrContext.Users.Where(e=>e.UserName == this.usrLogin).FirstOrDefault();          
            bool bAuthenticationState = user == null ? false : true;

            if(bAuthenticationState) {

                byte[] pswdHashWithSalt = user.PasswordHash;
				byte[] pswdSalt = user.Salt;

				byte[] pswdPure = Encoding.UTF8.GetBytes(this.usrPswd);
				byte[] pswdWithSalt = new byte[pswdPure.Length + pswdSalt.Length];

				Buffer.BlockCopy(pswdPure, 0, pswdWithSalt, 0, pswdPure.Length);
				Buffer.BlockCopy(pswdSalt, 0, pswdWithSalt, pswdPure.Length, pswdSalt.Length);

				SHA256Managed sha256 = new SHA256Managed();
				byte[] sha256PswdHash = sha256.ComputeHash(pswdHashWithSalt);

                bAuthenticationState = sha256PswdHash.SequenceEqual(pswdHashWithSalt);

            }

            return bAuthenticationState;
        }

    }
    
}