using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Services;

namespace WebProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly MemberServices _memberServices;
        private readonly OrderServices _orderServices;
        private readonly OrderDetailService _orderDetailServices;

        public OrdersController(GuestModelContext context, MemberServices memberServices, OrderServices orderServices, OrderDetailService orderDetailServices)
        {
            _context = context;
            _memberServices = memberServices;
            _orderServices = orderServices;
            _orderDetailServices = orderDetailServices;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string tag)
        {
            var acc = HttpContext.Session.GetString("Manager");
            List<Order> guestModelContext;
            if (tag == "1")
            {
                guestModelContext = await _orderServices.GetOrderListByAcc(acc);
                ViewData["ErrMsg"] = "您目前沒有未完成的訂單";
                ViewData["Title"] = "已下訂的訂單";
            }
            else if (tag == "2")
            {
                guestModelContext = await _orderServices.GetUCancelOrderListByAcc(acc);
                ViewData["ErrMsg"] = "您目前沒有已取消的訂單";
                ViewData["Title"] = "已取消的訂單";
            }
            else
            {
                guestModelContext = await _orderServices.GetBCancelOrderListByAcc(acc);
                ViewData["ErrMsg"] = "您目前沒有已完成的訂單";
                ViewData["Title"] = "已完成的訂單";
            }
            if (guestModelContext == null || guestModelContext[0] == null)
                guestModelContext.Clear();
            return View(guestModelContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .FirstOrDefaultAsync(m => m.OrderNo == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderNo,OrderDate,ShippingDate,IsGoodPackage,ShippingAddr,PayWayID,OrdertatusID,MemberID,ShippingWayID")] Order order)
        {
            if (id != order.OrderNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
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
            ViewData["MemberID"] = new SelectList(_context.Member, "MemberID", "MemberID", order.MemberID);
            ViewData["OrdertatusID"] = new SelectList(_context.Ordertatus, "OrdertatusID", "OrdertatusID", order.OrdertatusID);
            ViewData["PayWayID"] = new SelectList(_context.PayWay, "PayWayID", "PayWayID", order.PayWayID);
            ViewData["ShippingWayID"] = new SelectList(_context.ShippingWay, "ShippingWayID", "ShippingWayID", order.ShippingWayID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .FirstOrDefaultAsync(m => m.OrderNo == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelOrder(string orderNo)
        {
            await _orderServices.CancelOrder(orderNo);

            return RedirectToAction(nameof(Index), new { tag="1"});
        }

        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.OrderNo == id);
        }
    }
}
