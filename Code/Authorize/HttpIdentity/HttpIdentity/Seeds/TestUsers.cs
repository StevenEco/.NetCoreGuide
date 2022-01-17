namespace HttpIdentity.Seeds
{
    public class TestUsers
    {
        public static List<BAUser> GetBAUsers()
        {
            var users = new List<BAUser>()
            {
                new BAUser("bob","bob"),
                new BAUser("alice","alice")
            };
            return users;
        }
    }
}
