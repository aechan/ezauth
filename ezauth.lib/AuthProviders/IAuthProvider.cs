using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Security.Cryptography;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWT.Builder;

namespace ezauth.lib.AuthProviders {
    public abstract class IAuthProvider {

        /// <summary>
        /// Method to validate an user provided UserID and PassCode
        /// </summary>
        /// <param name="UserID">User identification e.g. 
        /// username, phone number, email etc..</param>
        /// <param name="PassCode">Password, TOTP code, HOTP code</param>
        /// <br/>
        /// <returns>AuthResult</returns>
        public abstract AuthorizationData Validate(string UserID, string PassCode);

        /// <summary>
        /// Method to retrieve all values that need to be stored to
        /// database for this AuthProvider.
        /// </summary>
        /// <returns></returns>
        public abstract string[] GetDataToStore();

        /// <summary>
        /// Generates a JWT token based off of the given data.
        /// 
        /// In the case of TOTP, HashedPassword can be the given code and
        /// UserSalt can be the shared secret.
        /// </summary>
        /// <param name="UserID">User's ID/Username</param>
        /// <param name="HashedPassword">Password/code</param>
        /// <param name="UserSalt">Saved user salt or secret</param>
        /// <returns>JWT token</returns>
        public string GenerateToken(string UserID, string HashedPassword, string UserSalt) {
            if(string.IsNullOrEmpty(UserID) ||
                string.IsNullOrEmpty(HashedPassword) ||
                string.IsNullOrEmpty(UserSalt)) {
                    throw new ArgumentException("None of the arguments can be null or empty");
            }
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(UserSalt)
                .AddClaim("Username", UserID)
                .AddClaim("PasswordHash", HashedPassword)
                .AddClaim("exp", DateTime.UtcNow.AddHours(3))
                .Build();
            return token;
        }

        public bool ValidateToken(string Token) {
            
        }
    }
}