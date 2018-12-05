namespace JDA.Entities.Response
{
    public class ResponseBase    
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TokenStatus TokenStatus { get; set; }
    }
}
