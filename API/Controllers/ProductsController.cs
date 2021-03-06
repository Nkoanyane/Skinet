using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.DTOs;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> ProductRepo;

        private readonly IGenericRepository<ProductBrand> ProductBrandsRepo;
        private readonly IGenericRepository<ProductType> ProductTypesRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo,
        IGenericRepository<ProductBrand> ProductBrandsRepo,
         IGenericRepository<ProductType> ProductTypesRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this.ProductTypesRepo = ProductTypesRepo;
            this.ProductBrandsRepo = ProductBrandsRepo;
            this.ProductRepo = ProductRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> getProducts()
        {
            var spec = new ProductsWithBrandsAndTypesSpecification();
            var Products = await this.ProductRepo.ListAsync(spec);
            return Ok(mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDTO>>(Products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]


        public async Task<ActionResult<ProductToReturnDTO>> getProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);
            var Product = await this.ProductRepo.GetEntityWithSpec(spec);
            if (Product==null) return NotFound(new ApiResponse(404));
            return mapper.Map<Product,ProductToReturnDTO>(Product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands()
        {
            return Ok(await this.ProductBrandsRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await this.ProductTypesRepo.ListAllAsync());
        }

    }
}
