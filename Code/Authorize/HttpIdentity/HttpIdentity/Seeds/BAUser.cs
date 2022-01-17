namespace HttpIdentity.Seeds
{
    public class BAUser
    {
        public BAUser(string username,string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
