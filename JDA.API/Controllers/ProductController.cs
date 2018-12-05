using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JDA.BusinessLayer.Contracts;
using JDA.Entities;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JDA.API.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _manager;
        private readonly IHttpContextAccessor _context;

        public ProductController(IProductManager manager, IHttpContextAccessor context)
        {
            _manager = manager;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<ProductsAndFacilities>), (int)HttpStatusCode.OK)]
        [Route("api/Product")]
        public async Task<IActionResult> Get()
        {
            var response = await _manager.GetProductsAndFacilities(_context);
            return Ok(new ServiceResponse<ProductsAndFacilities> { TokenStatus = TokenStatus.Valid, IsSuccess = true, Data = response });
        }
    }
}