[![NuGet version](https://badge.fury.io/nu/ezauth.lib.svg)](https://badge.fury.io/nu/ezauth.lib)
# ezauth
Dead simple user authentication for C# with support for 2FA.

## Usage
- Add reference to ezauth.lib project.
- Or install via Nuget `Install-Package ezauth.lib`
  ### TOTP Example
  TOTP - Time-based One-Time Password is an algorithm that generates a one-time password based on a secret key and the current time.

  Defined in [IETF RFC 6238](https://tools.ietf.org/html/rfc6238), TOTP is usually used as a second factor in a 2FA setup.

  Users will need to have a TOTP client like Google Authenticator.
  
  #### Code Example
  ````csharp
  using ezauth.lib.AuthProviders;
  
  // init new totp provider
  TOTPProvider totp = new TOTPProvider("ezauth_totp_test");
  
  // get the otp uri for sharing the secret with
  // a client e.g. Google Authenticator
  // Usually you'll encode this into a QR code (try QRCoder)
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
