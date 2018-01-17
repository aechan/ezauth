using System;

namespace ezauth.lib.AuthProviders {
    public interface IAuthProvider {

        /// <summary>
        /// Method to validate an user provided UserID and PassCode
        /// </summary>
        /// <param name="UserID">User identification e.g. 
        /// username, phone number, email etc..</param>
        /// <param name="PassCode">Password, TOTP code, HOTP code</param>
        /// <br/>
        /// <returns>AuthResult</returns>
        AuthResult Validate(string UserID, string PassCode);
    }
}