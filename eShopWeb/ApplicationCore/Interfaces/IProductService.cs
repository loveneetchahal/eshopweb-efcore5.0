using ApplicationCore.Dto;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
   public interface IProductService
    {
       // Task<DatabaseResponse> CreateProductsAsync(List<ProductCreateDto> products);
        Task<DatabaseResponse> CreateProductAsync(Product product);
        Task<DatabaseResponse> UpdateProductAsync(Product product);
        Task<DatabaseResponse> DeleteProductAsync(string productIds);
        Task<DatabaseResponse> GetProductByIdAsync(int productId);
        Task<DatabaseResponse> GetProductByVendorIdAsync(int vendorId);
        Task<DatabaseResponse> GetProductsAsync();
    }
}
