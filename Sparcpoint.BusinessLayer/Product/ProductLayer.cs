using Sparcpoint.Models;
using Sparcpoint.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sparcpoint.Models.DomainDto.Product;
using Sparcpoint.Mappers.DomainToEntity;
using System.Linq;
using Sparcpoint.Models.DomainModels;

namespace Sparcpoint.BusinessLayer.Product
{
    public class ProductLayer :IProductLayer
    {
        private readonly IProductRepository _productRepository;
        public ProductLayer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Products> AddProduct(ProductDomain product) {
            var productEntity  = ProductEntityMapper.MapDomainToEntity(product);
            productEntity.Attributes.ToList().ForEach(x => x.InstanceId = productEntity.InstanceId);
            productEntity.Categories.ToList().ForEach(x => x.InstanceId = productEntity.InstanceId);
            var productAdded = await _productRepository.AddProduct(productEntity);
            return productAdded;
        }
        public async Task<List<Products>> SearchProduct(FilterParam filterModel) {
            var productFiltered = await _productRepository.SearchProduct(filterModel);
            return productFiltered;
        }
    }
}
