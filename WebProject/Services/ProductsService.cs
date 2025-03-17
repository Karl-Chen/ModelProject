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

        public async Task<VMProducts> GetVMProductByPSID(string SpecificationID, bool isAll)
        {
            VMProducts vMProducts = new VMProducts()
            {
                Products = await _context.Prodect.Include(c => c.Brand)
                .Include(c => c.ProductSpecification)
                .Include(c => c.ProductType)
                .Where(s => s.ProductSpecificationID == SpecificationID || isAll).OrderByDescending(t => t.ProductTypeID).ToListAsync(),
                ProductSpecifications = await _context.ProductSpecification.OrderBy(t => t.SpecificationName).ToListAsync(),
            };
            return vMProducts;
        }

        public async Task<List<Product>> GetNeedReplenish()
        {
            var p = await _context.Prodect.Include(p => p.Brand)
                .Include(p => p.ProductSpecification)
                .Include(p => p.ProductType)
                .Include(p => p.Supplier)
                .Where(s => s.Inventory < 5).OrderByDescending(t => t.ProductTypeID).ToListAsync();
            return p;
        }

        public async Task<List<Product>> GetProductListDetailListByPSID(string SpecificationID, bool isAll)
        {
            var p = await _context.Prodect.Include(p => p.Brand)
                .Include(p => p.ProductSpecification)
                .Include(p => p.ProductType)
                .Include(p => p.Supplier)
                .Where(s => s.ProductSpecificationID == SpecificationID || isAll).OrderByDescending(t => t.ProductTypeID).ToListAsync();
            return p;
        }

        public async Task<List<ProductSpecification>> GetProductSpecificationList()
        {
            return await _context.ProductSpecification.OrderBy(t => t.SpecificationName).ToListAsync();
        }

        public async Task<Product> GetProductByID(string id)
        {
            return await _context.Prodect.FirstOrDefaultAsync(m => m.ProductID == id);
        }
        public async Task<Product> GetProductDetailByID(string id)
        {
            return await _context.Prodect
                .Include(p => p.Brand)
                .Include(p => p.ProductSpecification)
                .Include(p => p.ProductType)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductID == id);
        }

        public async Task CancelOrder(string productID, int count)
        {
            var p = await _context.Prodect
                .FirstOrDefaultAsync(m => m.ProductID == productID);
            p.Inventory += count;
            _context.Update(p);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductSpecification>> GetProductSpecification()
        {
            return await _context.ProductSpecification.OrderBy(b => b.SpecificationName).ToListAsync();
        }

        public async Task UpDateProduct(Product product)
        {
            var existingProduct = await _context.Prodect.FindAsync(product.ProductID);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).State = EntityState.Detached;
            }
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(string  id)
        {
            var product = await _context.Prodect.FindAsync(id);
            if (product != null)
            {
                _context.Prodect.Remove(product);
            }
            await _context.SaveChangesAsync();
        }
    }
}
