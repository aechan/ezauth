# ezauth
Dead simple user authentication for C# with support for 2FA.

## Usage
- Add reference to ezauth.lib project.
  ### TOTP Example
  ````csharp
  // init new totp provider
  TOTPProvider totp = new TOTPProvider("ezauth_totp_test");
  
  // get the otp uri for sharing the secret with
  // a client e.g. Google Authenticator
  string URI = totp.GetOTPURI();

  // validate the code given by the user for authentication
  AuthorizationData res = totp.Validate(user.username, input);

  // get the result of the Validate operation
  if (res.Result == AuthResult.VALIDATED) {
      Console.WriteLine("Your code was valid!");
      string token = res.AuthToken; // your JWT token for login later
      
  } else if (res.Result == AuthResult.INVALID_PASSCODE) {
      Console.WriteLine("Invalid code.");
  }
  ````
