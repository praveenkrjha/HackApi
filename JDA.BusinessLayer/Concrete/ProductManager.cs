using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JDA.BusinessLayer.Contracts;
using JDA.Common;
using JDA.Entities.Response;
using JDA.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;

namespace JDA.BusinessLayer.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly ConnectionStrings _connectionStrings;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;

        public ProductManager(ILogger logger, IOptions<AppSettings> appSettings, IOptions<ConnectionStrings> connectionStrings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _connectionStrings = connectionStrings.Value;
        }

        public async Task<ProductsAndFacilities> GetProductsAndFacilities(IHttpContextAccessor context)
        {
            try
            {
                var productsAndFacilities = new ProductsAndFacilities
                {
                    Facilities = new List<ProductDetail>(),
                    Products = new List<ProductDetail>()
                };
                using (IDalContext dalContext = new DalContext(_connectionStrings.HackConnection))
                {
                    var data = (await dalContext.DbConnection.QueryAsync<dynamic>("SELECT ProductId, ProductName, ProductDescription, LocationX, LocationY from ProductDetails", null, commandType: CommandType.Text)).Select(x => new ProductDetail
                    {
                        Id = x.ProductId,
                        Name = x.ProductName,
                        Description = x.ProductDescription,
                        LocationX = x.LocationX,
                        LocationY = x.LocationY
                    });

                    if (data.Any())
                    {
                        productsAndFacilities.Products.AddRange(data);
                    }

                    var data2 = (await dalContext.DbConnection.QueryAsync<dynamic>("SELECT FacilityId, FacilityName, FacilityDescription, LocationX, LocationY from Facilities", null, commandType: CommandType.Text)).Select(x => new ProductDetail
                    {
                        Id = x.FacilityId,
                        Name = x.FacilityName,
                        Description = x.FacilityDescription,
                        LocationX = x.LocationX,
                        LocationY = x.LocationY
                    });

                    if (data2.Any())
                    {
                        productsAndFacilities.Facilities.AddRange(data2);
                    }
                }

                return productsAndFacilities;
            }
            catch (Exception ex)
            {
                return null;
                Console.WriteLine(ex);
            }
        }
    }
}
