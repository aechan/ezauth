namespace ezauth.console.Models
{
    public class FakeUser {
        public string username;
        public string password;
        public string TOTPSecret;

        public FakeUser(string username, string password, string TOTPSecret) {
            this.username = username;
            this.password = password;
            this.TOTPSecret = TOTPSecret;
        }
    }
}