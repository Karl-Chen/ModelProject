using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.ViewModels;

namespace WebProject.Services
{
    public class ProductsService
    {
        private readonly GuestModelContext _context;
        public ProductsService(GuestModelContext context)
        {
            _context = context;
        }

        public async Task<VMProducts> GetProductListByPSID(string SpecificationID, bool isAll)
        {
            VMProducts vMProducts = new VMProducts()
            {
                Products = await _context.Prodect.Where(s => s.ProductSpecificationID == SpecificationID || isAll).OrderByDescending(t => t.ProductTypeID).ToListAsync(),
                ProductSpecifications = await _context.ProductSpecification.OrderBy(t => t.SpecificationName).ToListAsync(),
            };
            return vMProducts;
        }

        public async Task<Product> GetProductByID(string id)
        {
            return await _context.Prodect.FirstOrDefaultAsync(m => m.ProductID == id);
        }

        public async Task<List<ProductSpecification>> GetProductSpecification()
        {
            return await _context.ProductSpecification.OrderBy(b => b.SpecificationName).ToListAsync();
        }
    }
}
