using System;
using System.Security.Cryptography;

namespace ezauth.lib.AuthProviders {
    public class PasswordHashProvider : IAuthProvider
    {
        public override string[] GetDataToStore()
        {
            throw new NotImplementedException();
        }

        public override AuthorizationData Validate(string UserID, string PassCode)
        {
            throw new NotImplementedException();
        }
    }
}