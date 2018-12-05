namespace JDA.Entities.Response
{
    public class ServiceResponse<T> : ResponseBase
    {
        public T Data { get; set; }
    }
}
