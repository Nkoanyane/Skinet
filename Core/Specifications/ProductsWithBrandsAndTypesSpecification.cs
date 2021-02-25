using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification()
        {
            AddIncludes(x=>x.ProductBrand);
            AddIncludes(x=>x.ProductType);
        }

        public ProductsWithBrandsAndTypesSpecification(int id)
         : base(x=>x.Id==id)
        {
            AddIncludes(x=>x.ProductBrand);
            AddIncludes(x=>x.ProductType);
        }
    }
}