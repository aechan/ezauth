namespace ezauth.lib.AuthProviders {
    public enum AuthResult {
        VALIDATED,  // if the user is successfully authorized
        INVALID_USERNAME, // if the user provides an incorrect username
        INVALID_PASSCODE, // if the user provides an incorrect password/totp/hotp
        ERROR   // some other error occured
    }
}