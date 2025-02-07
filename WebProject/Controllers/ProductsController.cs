using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.ViewModels;
using System.IO;
using WebProject.Hubs;
using static WebProject.WorkFunction.FileIOFunction;
using Microsoft.AspNetCore.SignalR;

namespace WebProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly PushMessage _hubContext;

        public ProductsController(GuestModelContext context, IWebHostEnvironment env, PushMessage hubcontext)
        {
            _context = context;
            _env = env;
            _hubContext = hubcontext;
        }

        // GET: Products
        public async Task<IActionResult> Index(string SpecificationID = "00")
        {
            //var guestModelContext = _context.Prodect.Include(p => p.Brand).Include(p => p.ProductSpecification).Include(p => p.ProductType).Include(p => p.Supplier);
            //return View(await guestModelContext.ToListAsync());

            bool isAll = false;
            if (SpecificationID == "00")
                isAll = true;
            VMProducts vMProducts = new VMProducts()
            {
                Products = _context.Prodect.Where(s => s.ProductSpecificationID == SpecificationID || isAll).OrderByDescending(t => t.ProductTypeID).ToList(),
                ProductSpecifications = _context.ProductSpecification.OrderBy(t=>t.SpecificationName).ToList(),
            };
            if (vMProducts.Products.Count == 0)
                ViewData["ErrMsg"] = "該分類尚未建立商品";

            ViewData["isTest"] = "1234567899614521";

            if (HttpContext.Session.GetString("Manager") == null)
                ViewData["isLogin"] = "1";
            //ViewData["DeptName"] = db.Department.Find(deptid).DeptName;
            //ViewData["DeptID"] = deptid;

            return View(vMProducts);
        }

        // GET: Products/Details/5
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

            //ViewData["ProductName"] = product.ProductName;
            return View(product);
        }

        public async Task<IActionResult> AddOrderCarAsync(string pid, int value)
        {
            // TODO: 寫檔
            string path = _env.ContentRootPath + "/wwwroot/OrderCar";
            string account = HttpContext.Session.GetString("Manager").ToString();
            string fileName = account + ".txt";
            string orderdetail = pid + "," + value;
            WriteFileAppend(path, fileName, orderdetail);
            int count = ReadFileCount(path, fileName);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", account, count.ToString());
            return NoContent();
        }
        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order([Bind("BookID,SN,Title,Description,Author,TimeStmp,PhotoType,Photo")] Order order)
        {
            //if (id == null) {
            //    return NotFound();
            //}
            //var product = await _context.Prodect
            //    .Include(p => p.Brand)
            //    .Include(p => p.ProductSpecification)
            //    .Include(p => p.ProductType)
            //    .Include(p => p.Supplier)
            //    .FirstOrDefaultAsync(m => m.ProductID == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            var orderv = new Order();
            return View(orderv);
        }

        // GET: Products/Create
        //public IActionResult Create()
        //{
        //    ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID");
        //    ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID");
        //    ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID");
        //    ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID");
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ProductID,ProductName,Description,Photo,CostJP,CostExchangeRate,PriceExchangeRage,Inventory,OrderedQuantity,ProductTypeID,ProductSpecificationID,BrandID,SupplierID")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", product.BrandID);
        //    ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID", product.ProductSpecificationID);
        //    ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID", product.ProductTypeID);
        //    ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", product.SupplierID);
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Prodect.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", product.BrandID);
        //    ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID", product.ProductSpecificationID);
        //    ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID", product.ProductTypeID);
        //    ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", product.SupplierID);
        //    return View(product);
        //}

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("ProductID,ProductName,Description,Photo,CostJP,CostExchangeRate,PriceExchangeRage,Inventory,OrderedQuantity,ProductTypeID,ProductSpecificationID,BrandID,SupplierID")] Product product)
        //{
        //    if (id != product.ProductID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.ProductID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", product.BrandID);
        //    ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationID", product.ProductSpecificationID);
        //    ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeID", product.ProductTypeID);
        //    ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierID", product.SupplierID);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Prodect
        //        .Include(p => p.Brand)
        //        .Include(p => p.ProductSpecification)
        //        .Include(p => p.ProductType)
        //        .Include(p => p.Supplier)
        //        .FirstOrDefaultAsync(m => m.ProductID == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var product = await _context.Prodect.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Prodect.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(string id)
        //{
        //    return _context.Prodect.Any(e => e.ProductID == id);
        //}
    }
}
