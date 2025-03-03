using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebProject.Models;
using WebProject.ViewModels;

namespace WebProject.Services
{
    public class OrderDetailService
    {
        private readonly GuestModelContext _guestModelContext;
        private readonly ProductsService _productsService;
        private readonly OrderServices _orderServices;
        private readonly MemberServices _memberServices;


        public OrderDetailService(GuestModelContext guestModelContext, ProductsService productsService, OrderServices orderServices, MemberServices memberServices)
        {
            _guestModelContext = guestModelContext;
            _productsService = productsService;
            _orderServices = orderServices;
            _memberServices = memberServices;
        }

        public async Task WriteToOrderDetailTable(string orderNo, VMOrderCar vMOrderCar, float off = 0.0f)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in vMOrderCar.item)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductID = item.product;
                orderDetail.Vol = item.count;
                orderDetail.OrderNo = orderNo;
                orderDetail.Off = off;
                orderDetails.Add(orderDetail);
            }
            _guestModelContext.OrderDetail.AddRange(orderDetails);
            await _guestModelContext.SaveChangesAsync();
        }

        public async Task<VMOrderDetail> GetVMOrderDetailByAccAndOrderNo(string acc, string orderNo)
        {
            var memberID = await _memberServices.GetMemberIDByAccount(acc);
            var order = await _orderServices.GetOrderByOrderNo(orderNo);
            var listDetail = await GetOrderDetailByOrderNo(orderNo);
            return await GetVMOrderDetailByOrderObject(order, listDetail);
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderNo(string orderNo)
        {
            var list = await _guestModelContext.OrderDetail.Where(c=> c.OrderNo == orderNo).ToListAsync();
            return list;
        }

        private async Task<VMOrderDetail> GetVMOrderDetailByOrderObject(Order order, List<OrderDetail> orderDetail)
        {
            VMOrderDetail vMOrderDetail = new VMOrderDetail();
            vMOrderDetail.OrderDate = order.OrderDate;
            vMOrderDetail.ShippingDate = order.ShippingDate;
            vMOrderDetail.sendWay = order.ShippingWay.ShippingWayName;
            vMOrderDetail.ShippingAddr = order.ShippingAddr;
            vMOrderDetail.OrderStatusID = order.Ordertatus.OrdertatusName;
            vMOrderDetail.item = new List<VMOrderCarItem>();
            foreach (var it in orderDetail)
            {
                var p = await _productsService.GetProductByID(it.ProductID);
                VMOrderCarItem item1 = new VMOrderCarItem();
                float price = p.CostJP * p.PriceExchangeRage;
                item1.price = Convert.ToInt32((decimal)price);
                item1.product = it.ProductID;
                item1.name = p.ProductName;
                item1.img = p.Photo;
                item1.offset = it.Off;
                item1.count = it.Vol;
                vMOrderDetail.item.Add(item1);
            }

            return vMOrderDetail;
        }
    }
}
