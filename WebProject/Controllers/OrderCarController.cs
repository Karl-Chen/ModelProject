using Microsoft.AspNetCore.Mvc;
using WebProject.Services;
using WebProject.ViewModels;
using WebProject.WorkFunction;

namespace WebProject.Controllers
{
    public class OrderCarController : Controller
    {
        private readonly OrderCarServices _orderCarServices;

        public OrderCarController(OrderCarServices orderCarServices)
        {
            _orderCarServices = orderCarServices;
        }

        public async Task<IActionResult> Index()
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


            return View(ret);
        }
    }
}
