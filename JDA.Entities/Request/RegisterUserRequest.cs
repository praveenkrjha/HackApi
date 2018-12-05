namespace JDA.Entities.Request
{
    public class RegisterUserRequest
    {        
        public string EmailId { get; set; }
        public string Password { get; set; }               
   
        public string DeviceModel { get; set; }
        public string DeviceManufacturer { get; set; }
        public string DeviceRegistrationId { get; set; }
    }
}
