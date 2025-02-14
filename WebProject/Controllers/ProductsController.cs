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
//using WebProject.Hubs;
using static WebProject.WorkFunction.FileIOFunction;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using WebProject.WorkFunction;
using WebProject.Services;

namespace WebProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly FileIOFunction _fileIOFunction;
        private readonly ProductsService _productsService;
        private readonly MemberServices _memberServices;
        //private readonly IHubContext<PushMessage> _hubContext;

        //public ProductsController(GuestModelContext context, IWebHostEnvironment env, IHubContext<PushMessage> hubcontext)
        public ProductsController(GuestModelContext context, FileIOFunction fileIOFunction, ProductsService productsService, MemberServices memberServices)
        {
            _context = context;
            _fileIOFunction = fileIOFunction;
            _productsService = productsService;
            _memberServices = memberServices;
            //_hubContext = hubcontext;
        }

        // GET: Products
        public async Task<IActionResult> Index(string SpecificationID = "00")
        {
            //var guestModelContext = _context.Prodect.Include(p => p.Brand).Include(p => p.ProductSpecification).Include(p => p.ProductType).Include(p => p.Supplier);
            //return View(await guestModelContext.ToListAsync());

            bool isAll = false;
            if (SpecificationID == "00")
                isAll = true;
            
            VMProducts vMProducts = await _productsService.GetProductListByPSID(SpecificationID, isAll);
            if (vMProducts.Products.Count == 0)
                ViewData["ErrMsg"] = "該分類尚未建立商品";


            if (HttpContext.Session.GetString("Manager") == null)
                ViewData["isLogin"] = "1";

            return View(vMProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id, int mValue = 1, bool isShowModal = false)
        {
            GetOrderCarCount();
            if (id == null)
            {
                return NotFound();
            }
            ViewData["value"] = mValue;
            if (isShowModal)
                ViewData["isShowModal"] = "1";
            else
                ViewData["isShowModal"] = "0";
            //var product = await _context.Prodect
            //    .Include(p => p.Brand)
            //    .Include(p => p.ProductSpecification)
            //    .Include(p => p.ProductType)
            //    .Include(p => p.Supplier)
            //    .FirstOrDefaultAsync(m => m.ProductID == id);
            var product = await _productsService.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }

            //ViewData["ProductName"] = product.ProductName;
            return View(product);
        }

        public  IActionResult AddOrderCarAsync(string pid, int value)
        {
            // TODO: 寫檔
            var varacc = HttpContext.Session.GetString("Manager");
            if (varacc == null)
            {
                string id = pid;
                return RedirectToAction("Login", "Login", new { uAction = "Products", uRoute = "Details", pid = id });
            }
            string account = varacc.ToString();
            string fileName = account + ".txt";
            string orderdetail = pid + "," + value;
            _fileIOFunction.WriteFileAppend(fileName, orderdetail);
            // 本來想用聊天室的做法來發送push讓JS去更新畫面，但不知道為什麼JS進不去signalR的event裡面，所以作罷
            //_hubContext.Clients.All.SendAsync("SendMessage", account, count.ToString());
            return RedirectToAction("Details", "Products", new { id = pid, mValue = value, isShowModal = true});
            //return NoContent();
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

        // 取得購物車數量
        private string GetOrderCarCount()
        {
            var varacc = HttpContext.Session.GetString("Manager");
            if (varacc == null)
            {
                return null;
            }
            string account = varacc.ToString();
            string fileName = account + ".txt";
            int count = _fileIOFunction.ReadFileCount(fileName);
            HttpContext.Session.SetString("OrderCar", count.ToString());
            return varacc;
        }
    }
}
