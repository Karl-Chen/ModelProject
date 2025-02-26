using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using WebProject.Services;
using WebProject.ViewModels;
using WebProject.WorkFunction;

namespace WebProject.Controllers
{
    public class OrderCarController : Controller
    {
        private readonly OrderCarServices _orderCarServices;
        private readonly FileIOFunction _fileIOFunction;
        private readonly OrderServices _orderServices;
        private readonly OrderDetailService _orderDetailService;

        public OrderCarController(OrderCarServices orderCarServices, FileIOFunction fileIOFunction, OrderServices orderServices, OrderDetailService orderDetailService)
        {
            _orderCarServices = orderCarServices;
            _fileIOFunction = fileIOFunction;
            _orderServices = orderServices;
            _orderDetailService = orderDetailService;
        }

        public async Task<IActionResult> Index(string? sendWay, string? isFix)
        {
            var acc = HttpContext.Session.GetString("Manager");
            if (acc == null || acc == "")
                return View();
            acc += ".txt";
            VMOrderCar ret = await _orderCarServices.GetOrderCarList(acc);
            if (ret == null || ret.item == null || ret.item.Count() == 0)
            {
                ViewData["Message"] = "您的購物車裡還沒有任何商品。!";
            }
            if (sendWay != null && sendWay != "")
                ret.sendWay = sendWay;
            else
                ret.sendWay = "1";
            if (isFix != null && isFix != "")
                ret.isFix = isFix;
            else
                ret.isFix = "0";
                return View(ret);
        }

        public async Task<IActionResult> Delete(int index)
        {
            var acc = HttpContext.Session.GetString("Manager");
            if (acc == null || acc == "")
                return View();
            acc += ".txt";
            VMOrderCar ret = await _orderCarServices.GetOrderCarList(acc);
            if (ret == null || ret.item == null || ret.item.Count() == 0)
            {
                ViewData["Message"] = "您的購物車裡還沒有任何商品。!";
            }
            ret.item.RemoveAt(index - 1);
            var varacc = HttpContext.Session.GetString("Manager");
            string account = varacc.ToString();
            string fileName = account + ".txt";
            
            await _fileIOFunction.WriteFileOverWrite(fileName, ret);
            return RedirectToAction("Index", "OrderCar", new {  });

        }

        public async Task<IActionResult> RecordOrderCarItem(string pid, string value, string msendWay, string misFix)
        {
            var acc = HttpContext.Session.GetString("Manager");
            if (acc == null || acc == "")
                return NoContent();
            acc += ".txt";
            VMOrderCar ret = await _orderCarServices.GetOrderCarList(acc);
            if (ret == null || ret.item == null || ret.item.Count() == 0)
            {
                ViewData["Message"] = "您的購物車裡還沒有任何商品。!";
            }
            int intValue = int.Parse(value);
            
            for (int i = 0; i < ret.item.Count; i++)
            {
                if (ret.item[i].product == pid)
                {
                    if (intValue <= 0)
                    {
                        ret.item.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        ret.item[i].count = intValue;
                    }
                }
            }
            
            
            var varacc = HttpContext.Session.GetString("Manager");
            string account = varacc.ToString();
            string fileName = account + ".txt";

            await _fileIOFunction.WriteFileOverWrite(fileName, ret);
            return RedirectToAction("Index", "OrderCar", new { sendWay = msendWay, isFix = misFix });
        }
        //[HttpGet]
        public IActionResult mapCallbackFun(string storeName, string storeId)
        {
            ViewData["StoreName"] = storeName;
            ViewData["StoreID"] = storeId;
            return View();
        }

        public IActionResult mapOtherCallbackFun(string stName, string stCode)
        {
            ViewData["StoreName"] = stName;
            ViewData["StoreID"] = stCode;
            return View();
        }

        public async Task<IActionResult> SubmitOrder(string? ShippingAddr, string? IsGoodPackage)
        {
            var acc = HttpContext.Session.GetString("Manager");
            if (acc == null || acc == "")
                return NoContent();
            string orderNo = await _orderServices.WriteToOrderTable(ShippingAddr, IsGoodPackage, acc);
            acc += ".txt";
            VMOrderCar ret = await _orderCarServices.GetOrderCarList(acc);
            await _orderDetailService.WriteToOrderDetailTable(orderNo, ret);
            await _fileIOFunction.DeletOrderCardFile(acc);
            return RedirectToAction("Index", "OrderCar", new { });
        }
    }
}
