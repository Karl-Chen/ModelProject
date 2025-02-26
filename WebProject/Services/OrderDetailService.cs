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

        public OrderDetailService(GuestModelContext guestModelContext, ProductsService productsService)
        {
            _guestModelContext = guestModelContext;
            _productsService = productsService;
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
    }
}
