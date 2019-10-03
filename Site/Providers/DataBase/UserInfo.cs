namespace Demo.Providers.DataBase
{
    public class UserInfo : Entity
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}
