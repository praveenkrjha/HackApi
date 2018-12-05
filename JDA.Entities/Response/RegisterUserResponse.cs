namespace JDA.Entities.Response
{
    public class RegisterUserResponse
    {
        public string SecurityToken { get; set; }
        public UserDetails UserDetails { get; set; }
    }
}
