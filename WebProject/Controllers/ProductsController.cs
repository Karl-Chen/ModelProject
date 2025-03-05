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
            List<string> orderList = _fileIOFunction.ReadFileContent(fileName);
            bool isSame = false;
            for (int i = 0; i < orderList.Count(); i++)
            {
                string[] items = orderList[i].Split(",");
                if (items[0] == pid)
                {
                    int mValue = int.Parse(items[1]) + value;
                    string strValue = mValue.ToString();
                    orderList[i] = pid + "," + strValue;
                    isSame = true;
                    break;
                }
            }
            if (!isSame)
            {
                string orderdetail = pid + "," + value;
                _fileIOFunction.WriteFileAppend(fileName, orderdetail);
            }
            else
            {
                _fileIOFunction.WriteFileOverWrite(fileName, orderList);
            }
                // 本來想用聊天室的做法來發送push讓JS去更新畫面，但不知道為什麼JS進不去signalR的event裡面，所以作罷
                //_hubContext.Clients.All.SendAsync("SendMessage", account, count.ToString());
                return RedirectToAction("Details", "Products", new { id = pid, mValue = value, isShowModal = true });
            //return NoContent();
        }

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
