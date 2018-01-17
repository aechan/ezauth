using System;
using QRCoder;

using ezauth.lib.AuthProviders;

namespace ezauth.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the ezauth testbench.");
            Console.WriteLine("Options: 'totp' for TOTP test, 'hash' for Password Hashing test, 'q' to quit");
            Console.Write("> ");
            string input = Console.ReadLine();

            switch(input.ToLower()) {
                case "totp":
                    TestTOTP();
                    break;
                case "hash":
                    TestHash();
                    break;
            }
        }

        private static void TestHash()
        {
            throw new NotImplementedException();
        }

        private static void TestTOTP() {
            Console.WriteLine("======================== TOTP Test ========================");
            Console.WriteLine("Creating new user with data:");
            var user = new Models.FakeUser("John", "hunter2", "");
            Console.WriteLine("Username: " + user.username);
            Console.WriteLine("Password: " + user.password);
            
            TOTPProvider totp = new TOTPProvider("ezauth_totp_test");
            user.TOTPSecret = totp.SecretKey;

            Console.WriteLine("TOTPSecret: " + user.TOTPSecret);

            Console.WriteLine("\nPlease scan this code into your authenticator: \n");

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(totp.GetOTPURI(), QRCodeGenerator.ECCLevel.M);
            AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);

            string qrCodeAsAsciiArt = qrCode.GetGraphic(1, "  ", "██");
            Console.WriteLine(qrCodeAsAsciiArt);

            Console.WriteLine("\nYou now may enter codes from your authenticator to test");
            Console.WriteLine("Enter 'q' to stop.");

            string input;
            Console.Write("> ");
            while((input = Console.ReadLine()) != "q")            
            {
                var res = totp.Validate(user.username, input);
                if(res == AuthResult.VALIDATED) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Valid code!");
                    Console.ForegroundColor = ConsoleColor.White;
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid code");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("> ");
            }
        }

    }
}
