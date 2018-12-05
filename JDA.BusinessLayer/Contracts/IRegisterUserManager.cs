using JDA.Entities.Request;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JDA.BusinessLayer.Contracts
{
    public interface IRegisterUserManager
    {
        Task<ServiceResponse<RegisterUserResponse>> RegisterUser(IHttpContextAccessor context, RegisterUserRequest request);
    }
}
