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

        //public async Task<VMOrderDetail> GetVMOrderDetailByAccAndOrderNo(string acc, string orderNo)
        //{
        //    var memberID = await _memberServices.GetMemberIDByAccount(acc);
        //    var order = await _orderServices.GetOrderByOrderNo(orderNo);
            
        //}

        //public async Task<List<OrderDetail>> GetOrderDetailBy(string memberID, string orderNo)
        //{
        //    var list = await _guestModelContext.OrderDetail.
        //}
    }
}
