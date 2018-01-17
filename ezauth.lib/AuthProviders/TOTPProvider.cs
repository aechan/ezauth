using System;
using OtpNet;

namespace ezauth.lib.AuthProviders {
    /// <summary>
    /// AuthProvider that implements the TOTP standard.
    /// 
    /// Usually used as a 2FA factor but can be used as a
    /// standalone password.
    /// </summary>
    public class TOTPProvider : IAuthProvider
    {
        private Totp Totp;
        
        private string Provider;
        public string SecretKey;

        /// <summary>
        /// The default constructor creates a new secret key and saves it to the property SecretKey.
        /// Used for registering a new user. Make sure to save SecretKey to your database under your user.
        /// </summary>
        /// <param name="Provider">The provider name for encoding in a QR code: e.g. yoursite.com</param>
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
        /// <param name="Provider">The provider name for encoding in a QR code: e.g. yoursite.com</param>
        /// <param name="Base32Secret">The shared secret key</param>
        public TOTPProvider(string Provider, string Base32Secret) {
            this.Provider = Provider;
            SecretKey = Base32Secret;
            var base32Bytes = Base32Encoding.ToBytes(SecretKey);

            // init a totp class with this client's key
            Totp = new Totp(base32Bytes);
        }

        /// <summary>
        /// The OTP URI for adding this TOTP secret to an authenticator app.
        /// 
        /// Usually encoded as a QR code.
        /// </summary>
        /// <returns></returns>
        public string GetOTPURI() {
            return "otpauth://totp/"+Provider+"?secret="+SecretKey;
        }
        
        public override AuthorizationData Validate(string UserID, string PassCode)
        {
            if(Totp == null) {
                // if somehow the constructor wasnt called.......
                return new AuthorizationData(AuthResult.ERROR, "");
            } else {
                long timeStepMatched;
                if(Totp.VerifyTotp(PassCode, out timeStepMatched)) {
                    return new AuthorizationData(AuthResult.VALIDATED, GenerateToken(UserID, PassCode, SecretKey));
                } else {
                    return new AuthorizationData(AuthResult.INVALID_PASSCODE, "");
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>SecretKey at index 0.
        /// Save this linked to the user who created it.</returns>
        public override string[] GetDataToStore()
        {
            return new string[] {SecretKey};
        }
    }
}