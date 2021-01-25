using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProductService(IAsyncRepository<Product> productRepository, IMapper mapper, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<DatabaseResponse> CreateProductAsync(Product product)
        {
            var result = await _productRepository.AddAsync(product);
            int status = 0;
            if (result.Id != 0)
            {
                status = (int)DbReturnValue.CreateSuccess;
            }
            //if category exists to do 
            // status = (int)DbReturnValue.RecordExists
            return new DatabaseResponse { ResponseCode = status };
        }

        public async Task<DatabaseResponse> CreateProductsAsync(List<Product> products)
        {
            int result = await _productRepository.AddAllAsync(products);
            int status = 0;
            if (result > 0)
            {
                status = (int)DbReturnValue.CreateSuccess;
            }
            //if category exists to do 
            // status = (int)DbReturnValue.RecordExists
            return new DatabaseResponse { ResponseCode = status };
        }

        public async Task<DatabaseResponse> DeleteProductAsync(string productIds)
        {
            List<int> ids = productIds.Split(',').Select(int.Parse).ToList();
            int affectedrows = await _productRepository.DeleteAllAsync(ids);
            int status = 0;
            if (affectedrows > 0)
            {
                status = (int)DbReturnValue.DeleteSuccess;
            }
            else
            {
                status = (int)DbReturnValue.NotExists;
            }

            return new DatabaseResponse { ResponseCode = status };
        }

        public async Task<DatabaseResponse> GetProductByIdAsync(int productId)
        {
            var productSpec = new ProductWithImagesSpecification(productId);
            var products = await _productRepository.ListAsync(productSpec);
            int status = 0;
            if (products != null)
            {
                status = (int)DbReturnValue.RecordExists;
            }

            return new DatabaseResponse { ResponseCode = status, Results = products };
        }

        public async Task<DatabaseResponse> GetProductByVendorIdAsync(int vendorId)
        {
            var productSpec = new ProductWithImagesSpecification(vendorId, "vendorId");
            var products = await _productRepository.ListAsync(productSpec);
            int status = 0;
            if (products != null)
            {
                status = (int)DbReturnValue.RecordExists;
            }

            return new DatabaseResponse { ResponseCode = status, Results = products };
        }

        public async Task<DatabaseResponse> GetProductsAsync()
        {
            var productSpec = new ProductWithImagesSpecification();
            var products = await _productRepository.ListAsync(productSpec);
            int status = 0;
            if (products != null)
            {
                status = (int)DbReturnValue.RecordExists;
            }

            return new DatabaseResponse { ResponseCode = status, Results = products };
        }

        public Task<DatabaseResponse> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
