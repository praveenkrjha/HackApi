using System.Threading.Tasks;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Http;

namespace JDA.BusinessLayer.Contracts
{
    public interface IProductManager
    {
        Task<ProductsAndFacilities> GetProductsAndFacilities(IHttpContextAccessor context);
    }
}
