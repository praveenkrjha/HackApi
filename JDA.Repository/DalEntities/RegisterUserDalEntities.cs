namespace JDA.Repository.DalEntities
{
    public class RegisterUserDalEntities
    {
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public string SecurityToken { get; set; }
    }  

}
