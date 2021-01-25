using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ProductService : IProductService
    {
        public Task<DatabaseResponse> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<DatabaseResponse> DeleteProductAsync(string productIds)
        {
            throw new NotImplementedException();
        }

        public Task<DatabaseResponse> GetProductByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<DatabaseResponse> GetProductByVendorIdAsync(int vendorId)
        {
            throw new NotImplementedException();
        }

        public Task<DatabaseResponse> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DatabaseResponse> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
