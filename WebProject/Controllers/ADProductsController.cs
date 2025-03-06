using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Filters;
using WebProject.Models;
using WebProject.Services;

namespace WebProject.Controllers
{
    [ServiceFilter(typeof(MemberStatusFilter))]
    public class ADProductsController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly ProductsService _productsService;

        public ADProductsController(GuestModelContext context, ProductsService productsService)
        {
            _context = context;
            _productsService = productsService;
        }

        // GET: ADProducts
        public async Task<IActionResult> Index(string SpecificationID = "00")
        {
            bool isAll = false;
            if (SpecificationID == "00")
                isAll = true;

            var p = await _productsService.GetProductListDetailListByPSID(SpecificationID, isAll);
            return View(p);
        }

        // GET: ADProducts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetProductDetailByID(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ADProducts/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandName");
            ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationName");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeName");
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierName");
            return View();
        }

        // POST: ADProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,Description,Photo,CostJP,CostExchangeRate,PriceExchangeRage,Inventory,OrderedQuantity,ProductTypeID,ProductSpecificationID,BrandID,SupplierID")] Product product, IFormFile? newPhoto)
        {
            if (newPhoto != null && newPhoto.Length != 0)
            {
                //執行上傳照片
                //只允許上傳圖檔
                if (newPhoto.ContentType != "image/jpeg" && newPhoto.ContentType != "image/png")
                {
                    ViewData["Message"] = "請上傳正確JPG或PNG格式";
                    return View(product);
                }
                string fileName = "PIC_" + product.ProductID + ".jpg";
                string BookPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ModelPhoto", fileName);
                using (FileStream stream = new FileStream(BookPhotoPath, FileMode.Create))
                {
                    newPhoto.CopyTo(stream);
                }
                product.Photo = fileName;
            }
            ModelState.Clear(); // 清除舊的 ModelState
            TryValidateModel(product); // 重新執行 Model 驗證
            if (ModelState.IsValid)
            {
                await _productsService.AddProduct(product);
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
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandName", product.BrandID);
            ViewData["ProductSpecificationID"] = new SelectList(_context.ProductSpecification, "SpecificationID", "SpecificationName", product.ProductSpecificationID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "TypeID", "TypeName", product.ProductTypeID);
            ViewData["SupplierID"] = new SelectList(_context.Supplier, "SupplierID", "SupplierName", product.SupplierID);
            return View(product);
        }

        // POST: ADProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProductID,ProductName,Description,Photo,CostJP,CostExchangeRate,PriceExchangeRage,Inventory,OrderedQuantity,ProductTypeID,ProductSpecificationID,BrandID,SupplierID")] Product product, IFormFile? newPhoto)
        {

            if (newPhoto != null && newPhoto.Length != 0)
            {
                //執行上傳照片
                //只允許上傳圖檔
                if (newPhoto.ContentType != "image/jpeg" && newPhoto.ContentType != "image/png")
                {
                    ViewData["Message"] = "請上傳正確JPG或PNG格式";
                    return View(product);
                }
                string fileName = "PIC_" + product.ProductID + ".jpg";
                string BookPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ModelPhoto", fileName);
                using (FileStream stream = new FileStream(BookPhotoPath, FileMode.Create))
                {
                    newPhoto.CopyTo(stream);
                }
                product.Photo = fileName;
            }
            ModelState.Clear(); // 清除舊的 ModelState
            TryValidateModel(product); // 重新執行 Model 驗證
            if (ModelState.IsValid)
            {
                try
                {
                    await _productsService.UpDateProduct(product);
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

            var product = await _productsService.GetProductDetailByID(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ADProducts/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _productsService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Prodect.Any(e => e.ProductID == id);
        }
    }
}
