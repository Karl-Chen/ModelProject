using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebProject.Models;

namespace WebProject.Services
{
    public class OrderServices
    {
        private readonly GuestModelContext _guestModelContext;
        private readonly ProductsService _productsService;
        private readonly MemberServices _memberService;
        private readonly StaffServices _staffService;

        public OrderServices(GuestModelContext guestModelContext, ProductsService productsService, MemberServices memberService, StaffServices staffService)
        {
            _guestModelContext = guestModelContext;
            _productsService = productsService;
            _memberService = memberService;
            _staffService = staffService;
        }

        public async Task<string> WriteToOrderTable(string ShippingAddr, string isGoodPackage, string acc, string OrderPhone, string OrderName)
        {
            Order order = new Order();
            string orderNumber = await GetOrderNumber();
            var m = await _memberService.GetMemberbyAcc(acc);
            string MemberID = m.MemberID;
            order.OrderNo = orderNumber;
            order.IsGoodPackage = isGoodPackage == "0" ? false : true;
            order.OrderDate = DateTime.Today;
            order.OrdertatusID = "01";
            order.ShippingAddr = ShippingAddr == "" || ShippingAddr == null ? "自取" : ShippingAddr;
            order.PayWayID = ShippingAddr == "" || ShippingAddr == null ? "0" : "1";
            if (OrderName == null || OrderName == "")
            {
                OrderName = m.Name;
            }
            if (OrderPhone == null || OrderPhone == "")
            {
                OrderPhone = m.MemberTel[0].TelNumber;
            }
            order.OrderName = OrderName;
            order.OrderPhone = OrderPhone;
            string ShippingWayID = "1";
            if (ShippingAddr == null || ShippingAddr == "")
                ShippingWayID = "1";
            else if (ShippingAddr.StartsWith("OK"))
                ShippingWayID = "5";
            else if (ShippingAddr.StartsWith("全家"))
                ShippingWayID = "3";
            else if (ShippingAddr.StartsWith("萊爾富"))
                ShippingWayID = "4";
            else if (ShippingAddr.StartsWith("7-11"))
                ShippingWayID = "2";
            order.ShippingWayID = ShippingWayID;
            order.MemberID = MemberID;

            _guestModelContext.Add(order);
            await _guestModelContext.SaveChangesAsync();

            return orderNumber;
        }

        public async Task<List<Order>> GetOrderListByAcc(string acc)
        {
            var memberID = await _memberService.GetMemberIDByAccount(acc);
            var OrderList = await _guestModelContext.Order.Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .Where(c => c.MemberID == memberID && !(c.OrdertatusID == "10" || c.OrdertatusID == "05" || c.OrdertatusID == "11"))
                .DefaultIfEmpty().ToListAsync();
            return OrderList;
        }

        public async Task<List<Order>> GetAllOrderList()
        {
            var OrderList = await _guestModelContext.Order.Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .Include(o=> o.Member.MemberTel)
                .Include(o => o.Member.MemberAcc)
                .Where(c => !(c.OrdertatusID == "10" || c.OrdertatusID == "05" || c.OrdertatusID == "11"))
                .DefaultIfEmpty().ToListAsync();
            return OrderList;
        }

        public async Task<List<Order>> GetUCancelOrderListByAcc(string acc)
        {
            var memberID = await _memberService.GetMemberIDByAccount(acc);
            var OrderList = await _guestModelContext.Order.Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .Where(c => c.MemberID == memberID && (c.OrdertatusID == "10" || c.OrdertatusID == "11"))
                .DefaultIfEmpty().ToListAsync();
            return OrderList;
        }

        public async Task<List<Order>> GetAllCancelOrderList()
        {
            var OrderList = await _guestModelContext.Order.Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .Include(o => o.Member.MemberTel)
                .Include(o => o.Member.MemberAcc)
                .Where(c => (c.OrdertatusID == "10" || c.OrdertatusID == "11"))
                .DefaultIfEmpty().ToListAsync();
            return OrderList;
        }

        public async Task<List<Order>> GetBCancelOrderListByAcc(string acc)
        {
            var memberID = await _memberService.GetMemberIDByAccount(acc);
            var OrderList = await _guestModelContext.Order.Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .Where(c => c.MemberID == memberID && (c.OrdertatusID == "05"))
                .DefaultIfEmpty().ToListAsync();
            return OrderList;
        }

        public async Task<List<Order>> GetAllBCancelOrderListstring()
        {
            var OrderList = await _guestModelContext.Order.Include(o => o.Member)
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .Include(o => o.Member.MemberTel)
                .Include(o => o.Member.MemberAcc)
                .Where(c => c.OrdertatusID == "11")
                .DefaultIfEmpty().ToListAsync();
            return OrderList;
        }


        public async Task<bool> CancelOrder(string orderNo)
        {
            var order = await _guestModelContext.Order.Where(c => c.OrderNo == orderNo).DefaultIfEmpty().FirstOrDefaultAsync();
            if (order == null)
                return false;
            if (order.ShippingDate == null)
            {
                // ShippingDate 為 null，可以設置預設值或進行其他處理
                order.ShippingDate = null; // 或者 DateTime.MinValue 根據需求
            }
            var list = await _guestModelContext.OrderDetail
                .Include(c => c.Products)
                .Where(c => c.OrderNo == orderNo)
                .DefaultIfEmpty()
                .ToListAsync();
            foreach (var item in list)
            {
                await _productsService.CancelOrder(item.ProductID, item.Vol);
            }
            order.OrdertatusID = "10";
            _guestModelContext.Update(order);
            await _guestModelContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrder(Order order, string acc)
        {
            _guestModelContext.Update(order);
            await _guestModelContext.SaveChangesAsync();
            string  staffID = await _staffService.GetStaffIDByAccount(acc);
            if (staffID != null)
            {
                HandleOrder handleOrder = new HandleOrder();
                handleOrder.StaffID = staffID;
                handleOrder.OrderNo = order.OrderNo;
                handleOrder.HandleTime = DateTime.Now;
                var oldHandleOrder = await _guestModelContext.HandleOrder.Where(c => c.OrderNo == order.OrderNo && c.StaffID == staffID).DefaultIfEmpty().FirstAsync();
                if (oldHandleOrder == null)
                {
                    _guestModelContext.HandleOrder.Add(handleOrder);
                    await _guestModelContext.SaveChangesAsync();
                }
                else
                {
                    oldHandleOrder.HandleTime = DateTime.Now;
                    _guestModelContext.Update(oldHandleOrder);
                    await _guestModelContext.SaveChangesAsync();
                }
            }
            var oldInvoice = await _guestModelContext.Invoice.Where(c => c.OrderNo == order.OrderNo).DefaultIfEmpty().FirstAsync();
            if (order.OrdertatusID == "04" && oldInvoice == null)
            {
                Invoice invoice = new Invoice();
                Random random = new Random(DateTime.Now.Ticks.GetHashCode());
                invoice.InvoiceNo = random.Next(0, 99999999).ToString().PadLeft(8, '0');
                invoice.OrderNo = order.OrderNo;
                _guestModelContext.Invoice.Add(invoice);
                await _guestModelContext.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrderByOrderNo(string orderNo)
        {
            var order = await _guestModelContext.Order
                .Include(o => o.Ordertatus)
                .Include(o => o.PayWay)
                .Include(o => o.ShippingWay)
                .Include(o => o.Member)
                .Include(o => o.Invoice)
                .Include(o => o.HandleOrder)
                .ThenInclude(o => o.Staff)
                .Where(c => c.OrderNo == orderNo)
                .DefaultIfEmpty()
                .FirstOrDefaultAsync();
            return order;
        }

        public async Task<string> GetMemberIDByOrderNo(string orderNo)
        {
            return await _guestModelContext.Order             
                .Where(c => c.OrderNo == orderNo)
                .Select(o => o.MemberID)
                .DefaultIfEmpty()
                .FirstOrDefaultAsync();
        }

        private async Task<string> GetOrderNumber()
        {
            //var memberID = await _context.Member.OrderByDescending(c => c.MemberID).Select(c => c.MemberID).FirstOrDefaultAsync();
            string oldOrderNumber = await _guestModelContext.Order.OrderByDescending(c => c.OrderNo).Select(c => c.OrderNo).FirstOrDefaultAsync();
            string oldOrderNumberDate = oldOrderNumber.Substring(1, 8);
            string oldOrderNumberCount = oldOrderNumber.Substring(9, 4);
            string today = DateTime.Today.ToString("yyyyMMdd");
            string ret = "N";
            if (int.Parse(today) > int.Parse(oldOrderNumberDate))
            {
                ret += today + "0001";
            }
            else
            {
                int index = int.Parse(oldOrderNumberCount);
                index++;
                string count = index.ToString("D4");
                ret += today + count;
            }
            return ret;

        }
    }
}
