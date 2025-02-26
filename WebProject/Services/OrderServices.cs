using Microsoft.EntityFrameworkCore;
using System;
using WebProject.Models;

namespace WebProject.Services
{
    public class OrderServices
    {
        private readonly GuestModelContext _guestModelContext;
        private readonly ProductsService _productsService;
        private readonly MemberServices _memberService;

        public OrderServices(GuestModelContext guestModelContext, ProductsService productsService, MemberServices memberService = null)
        {
            _guestModelContext = guestModelContext;
            _productsService = productsService;
            _memberService = memberService;
        }

        public async Task<string> WriteToOrderTable(string ShippingAddr, string isGoodPackage, string acc)
        {
            Order order = new Order();
            string orderNumber = await GetOrderNumber();
            string MemberID = await _memberService.GetMemberIDByAccount(acc);
            order.OrderNo = orderNumber;
            order.IsGoodPackage = isGoodPackage == "0" ? false : true;
            order.OrderDate = DateTime.Today;
            order.OrdertatusID = "01";
            order.ShippingAddr = ShippingAddr == "" || ShippingAddr == null ? "自取" : ShippingAddr;
            order.PayWayID = ShippingAddr == "" || ShippingAddr == null ? "0" : "1";
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
