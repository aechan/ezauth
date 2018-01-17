using System;
using System.Security.Cryptography;

namespace ezauth.lib.AuthProviders {
    public class PasswordHashProvider : IAuthProvider
    {
        AuthResult IAuthProvider.Validate(string UserID, string PassCode)
        {
            throw new NotImplementedException();
        }
    }
}