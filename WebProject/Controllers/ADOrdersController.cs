using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Services;
using WebProject.ViewModels;

namespace WebProject.Controllers
{
    public class ADOrdersController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly OrderServices _orderServices;
        private readonly OrderDetailService _orderDetailService;

        public ADOrdersController(GuestModelContext context, OrderServices orderServices, OrderDetailService orderDetailService)
        {
            _context = context;
            _orderServices = orderServices;
            _orderDetailService = orderDetailService;
        }

        // GET: ADOrders
        //public async Task<IActionResult> Index()
        //{
        //    var p = _context.Order.Include(o => o.Member).Include(o => o.Ordertatus).Include(o => o.PayWay).Include(o => o.ShippingWay).DefaultIfEmpty();
        //    return View(await p.ToListAsync());
        //}

        public async Task<IActionResult> Index(string tag = "1")
        {
            var acc = HttpContext.Session.GetString("Manager");
            List<Order> guestModelContext;
            if (tag == "1")
            {
                guestModelContext = await _orderServices.GetAllOrderList();
                ViewData["ErrMsg"] = "目前沒有未完成的訂單";
                ViewData["Title"] = "已下訂的訂單";
            }
            else if (tag == "2")
            {
                guestModelContext = await _orderServices.GetAllCancelOrderList();
                ViewData["ErrMsg"] = "目前沒有已取消的訂單";
                ViewData["Title"] = "已取消的訂單";
            }
            else
            {
                guestModelContext = await _orderServices.GetAllBCancelOrderListstring();
                ViewData["ErrMsg"] = "目前沒有已完成的訂單";
                ViewData["Title"] = "已完成的訂單";
            }
            if (guestModelContext == null || guestModelContext[0] == null)
                guestModelContext.Clear();
            return View(guestModelContext);
        }

        // GET: ADOrders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VMOrderDetail vMOrderDetail = await _orderDetailService.GetVMOrderDetailByAccAndOrderNo(id);
            if (vMOrderDetail == null)
            {
                ViewData["ErrMsg"] = "查無資料";
                return NotFound();
            }
            ViewData["OrderNo"] = id;
            return View(vMOrderDetail);
        }

        // GET: ADOrders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _orderServices.GetOrderByOrderNo(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["OrdertatusID"] = new SelectList(_context.Ordertatus, "OrdertatusID", "OrdertatusName");
            return View(order);
        }


        // 編輯訂單待續

        // POST: ADOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("OrderNo,OrderDate,ShippingDate,IsGoodPackage,ShippingAddr,PayWayID,OrdertatusID,MemberID,ShippingWayID")] Order order)
        {
            //if (id != order.OrderNo)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var acc = HttpContext.Session.GetString("Admin");
                    await _orderServices.UpdateOrder(order, acc);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderNo))
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

            ViewData["OrdertatusID"] = new SelectList(_context.Ordertatus, "OrdertatusID", "OrdertatusName", order.OrdertatusID);
            return View(order);
        }

       

        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.OrderNo == id);
        }
    }
}
