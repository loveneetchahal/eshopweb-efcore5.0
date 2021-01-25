using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public sealed class ProductWithImagesSpecification: Specification<Product>
    {
        public ProductWithImagesSpecification(int productId)
        {
            Query
                .Where(b => b.Id == productId)
                .Include(b => b.ProductImages);
        }
        public ProductWithImagesSpecification(int vendorId,string field= "")
        {
            Query
                .Where(b => b.VendorId == vendorId)
                .Include(b => b.ProductImages);
        }
        public ProductWithImagesSpecification()
        {
            Query
                .Include(b => b.ProductImages);
        }
    }
}
