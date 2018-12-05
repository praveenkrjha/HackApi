using System;
using System.Collections.Generic;
using System.Text;

namespace JDA.Entities.Response
{
    public class ProductsAndFacilities
    {
        public List<ProductDetail> Products { get; set; }

        public List<ProductDetail> Facilities { get; set; }
    }

    public class ProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int LocationX { get; set; }

        public int LocationY { get; set; }
    }
}
