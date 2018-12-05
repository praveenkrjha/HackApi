using System.Data;
using System.Threading.Tasks;
using JDA.Repository.DalRequest;
using JDA.Repository.DalResponse;

namespace JDA.Repository.Contracts
{
    public interface IValidateUserRepository
    {
        Task<ValidateDalResponse> ValidateUser(IDbConnection connection, ValidateDalRequest validateUserRequest);
    }
}
