using JDA.Entities.Request;
using JDA.Entities.Response;
using System.Threading.Tasks;

namespace JDA.BusinessLayer.Contracts
{
    public interface IValidateUserManager
    {
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="validateUserRequest"></param>
        /// <returns></returns>
        Task<ServiceResponse<ValidateUserResponse>> ValidateUser(ValidateUserRequest validateUserRequest);
    }
}
