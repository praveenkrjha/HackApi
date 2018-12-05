using JDA.Entities.Request;
using System.Data;
using System.Threading.Tasks;
using JDA.Repository.DalResponse;

namespace JDA.Repository.Contracts
{
    public interface IRegisterUserRepository
    {
        Task<RegisterUserDalResponse> RegisterUser(IDbConnection connection, RegisterUserRequest request, string encryptedToken, int userId);
    }
}
