
namespace ezauth.lib.AuthProviders {
    /// <summary>
    /// Data returned from a Validate operation
    /// </summary>
    public class AuthorizationData {
        public AuthResult Result;
        public string AuthToken;

        public AuthorizationData(AuthResult result, string AuthToken) {
            this.Result = result;
            this.AuthToken = AuthToken;
        }
    }

}