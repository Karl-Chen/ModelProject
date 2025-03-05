using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Filters;
using WebProject.Models;

namespace WebProject.Controllers
{
    [ServiceFilter(typeof(MemberStatusFilter))]
    public class ADProductsController : Controller
    {
        private readonly GuestModelContext _context;

        public ADProductsController(GuestModelContext context)
        {
            _context = context;
        }

        // GET: ADProducts
        public async Task<IActionResult> Index()
        {
            var guestModelContext = _context.Prodect.Include(p => p.Brand).Include(p => p.ProductSpecification).Include(p => p.ProductType).Include(p => p.Supplier);
            return View(await guestModelContext.ToListAsync());
        }

        // GET: ADProducts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Prodect
                .Include(p => p.Brand)
                .Include(p => p.ProductSpecification)
                .Include(p => p.ProductType)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ADProducts/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID");
            ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID");
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID");
            return View();
        }

        // POST: ADProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,Description,Photo,CostJP,CostExchangeRate,PriceExchangeRage,Inventory,OrderedQuantity,ProductTypeID,ProductSpecificationID,BrandID,SupplierID")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", product.BrandID);
            ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID", product.ProductSpecificationID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID", product.ProductTypeID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", product.SupplierID);
            return View(product);
        }

        // GET: ADProducts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Prodect.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", product.BrandID);
            ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID", product.ProductSpecificationID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID", product.ProductTypeID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", product.SupplierID);
            return View(product);
        }

        // POST: ADProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductID,ProductName,Description,Photo,CostJP,CostExchangeRate,PriceExchangeRage,Inventory,OrderedQuantity,ProductTypeID,ProductSpecificationID,BrandID,SupplierID")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", product.BrandID);
            ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID", product.ProductSpecificationID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID", product.ProductTypeID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", product.SupplierID);
            return View(product);
        }

        // GET: ADProducts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Prodect
                .Include(p => p.Brand)
                .Include(p => p.ProductSpecification)
                .Include(p => p.ProductType)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ADProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Prodect.FindAsync(id);
            if (product != null)
            {
                _context.Prodect.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Prodect.Any(e => e.ProductID == id);
        }
    }
}
