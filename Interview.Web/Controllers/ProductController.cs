using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparcpoint;
using Sparcpoint.BusinessLayer.Product;
using Sparcpoint.Mappers.DomainToEntity;
using Sparcpoint.Models;
using Sparcpoint.Models.DomainDto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Web.Controllers
{
    [Route("api/v1/products")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductLayer _productManager;
        private readonly IDataSerializer _serialize;
        public ProductController(IProductLayer productManager, IDataSerializer serialize)
        {
            //Dependency Injection - Configured Startup Class
            _productManager = productManager;
            _serialize = serialize;
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts([FromBody] ProductDto product)
        {
            var productList = await _productManager.AddProduct(ProductEntityMapper.MapDTOtoDomain(product));
            return (IActionResult)Ok(_serialize.Serialize<Products>(productList));
        }
        [HttpGet]
        public async Task<IActionResult> SearchProducts(string name, string description, string productImageUri, string validSkusGuid, string category)
        {
            //Creating FilterModel from the parameters passed
            FilterModel product = new FilterModel()
            {
                Name = name,
                Description = description,
                ProductImageUri = productImageUri,
                ValidSkus = validSkusGuid,
                Category = category
            };
            var productList = await _productManager.SearchProduct(product);
            return (IActionResult)Ok(_serialize.Serialize<List<Products>>(productList));
        }

    }
}
