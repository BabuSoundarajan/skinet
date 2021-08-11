using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        public StoreContext StoreContext { get; }
        public ProductRepository(StoreContext storeContext)
        {
            this.StoreContext = storeContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
         return await StoreContext.Products
        .Include(p=>p.ProductBrand)
        .Include(p=>p.ProductType)
         .FirstOrDefaultAsync(p=>p.Id==id);  
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await StoreContext.Products
            .Include(p=>p.ProductBrand)
            .Include(p=>p.ProductType)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await StoreContext.ProductBrand.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await StoreContext.ProductType.ToListAsync();
        }
    }
}