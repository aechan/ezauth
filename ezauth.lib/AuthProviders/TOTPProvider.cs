using System;
using OtpNet;

namespace ezauth.lib.AuthProviders {
    public class TOTPProvider : IAuthProvider
    {
        private Totp Totp;
        
        private string Provider;
        public string SecretKey;

        /// <summary>
        /// The default constructor creates a new secret key and saves it to the property SecretKey
        /// Used for registering a new user. Make sure to save SecretKey to your database under your user.
        /// </summary>
        public TOTPProvider(string Provider) {
            this.Provider = Provider;

            // generate random key and convert it to base32 byte array
            var key = KeyGeneration.GenerateRandomKey(20);

            var base32String = Base32Encoding.ToString(key);
            SecretKey = base32String;
            var base32Bytes = Base32Encoding.ToBytes(SecretKey);

            // init a totp class with this client's key
            Totp = new Totp(base32Bytes);
        }

        /// <summary>
        /// For users who have already set up TOTP and have a secret already generated.
        /// </summary>
        /// <param name="Base32Secret">The shared secret key</param>
        public TOTPProvider(string Provider, string Base32Secret) {
            this.Provider = Provider;
            SecretKey = Base32Secret;
            var base32Bytes = Base32Encoding.ToBytes(SecretKey);

            // init a totp class with this client's key
            Totp = new Totp(base32Bytes);
        }

        public string GetOTPURI() {
            return "otpauth://totp/"+Provider+"?secret="+SecretKey;
        }

        public AuthResult Validate(string UserID, string PassCode)
        {
            if(Totp == null) {
                throw new InvalidOperationException("TOTPSecret cannot be empty," +
                 "please set your shared secret key before trying to validate.");

                 // for some reason if you want to avoid throwing exceptions
                 // you can return an error
                 // return AuthResult.ERROR;
            } else {
                long timeStepMatched;
                if(Totp.VerifyTotp(PassCode, out timeStepMatched)) {
                    return AuthResult.VALIDATED;
                } else {
                    return AuthResult.INVALID_PASSCODE;
                }
            }
        }
    }
}