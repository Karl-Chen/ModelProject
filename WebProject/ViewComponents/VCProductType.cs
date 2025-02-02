using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.ViewComponents
{
    public class VCProductType:ViewComponent
    {
        private readonly GuestModelContext _context;
        public VCProductType(GuestModelContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _context.ProductSpecification.OrderBy(b => b.SpecificationName).ToArrayAsync();
            return View(books);
        }
    }
}
