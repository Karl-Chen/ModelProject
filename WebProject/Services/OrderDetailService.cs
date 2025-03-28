﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
            string memberID = await _orderServices.GetMemberIDByOrderNo(orderNo);
            int totalprice = 0;
            foreach (var item in vMOrderCar.item)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductID = item.product;
                orderDetail.Vol = item.count;
                orderDetail.OrderNo = orderNo;
                orderDetail.Off = off;
                orderDetails.Add(orderDetail);
                await UpdateProductQty(item.product, item.count);
                var p = await _productsService.GetProductByID(item.product);
                totalprice += (int)(p.CostJP * p.PriceExchangeRage * item.count);
            }
            _guestModelContext.OrderDetail.AddRange(orderDetails);
            await _guestModelContext.SaveChangesAsync();

            int point = totalprice / 100;
            await _memberServices.UpdateMemberPointByMemberID(memberID, point);
        }

        public async Task UpdateProductQty(string pid, int count)
        {
            var p = await _productsService.GetProductDetailByID(pid);
            p.Inventory = p.Inventory - count;
            p.OrderedQuantity = p.OrderedQuantity + count;
            await _productsService.UpDateProduct(p);
        }

        public async Task<VMOrderDetail> GetVMOrderDetailByAccAndOrderNo(string orderNo)
        {
            var order = await _orderServices.GetOrderByOrderNo(orderNo);
            var listDetail = await GetOrderDetailByOrderNo(orderNo);
            return await GetVMOrderDetailByOrderObject(order, listDetail);
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderNo(string orderNo)
        {
            var list = await _guestModelContext.OrderDetail
                .Include(c => c.Products)
                .Include(c => c.Order)
                .ThenInclude(c=>c.PayWay)
                .Include(c => c.Order)
                .ThenInclude(c => c.Invoice)
                .Include(c => c.Order)
                .ThenInclude(c => c.ShippingWay)
                .Include(c => c.Order)
                .ThenInclude(c => c.Ordertatus)
                .Include(c => c.Order)
                .ThenInclude(c => c.HandleOrder)
                .ThenInclude(c => c.Staff)
                .Where(c=> c.OrderNo == orderNo)
                .DefaultIfEmpty()
                .ToListAsync();
            return list;
        }

        public async Task CancelOrder(string orderNo)
        {
            var list = await _guestModelContext.OrderDetail
                .Include(c => c.Products)
                .Where(c => c.OrderNo == orderNo)
                .DefaultIfEmpty()
                .ToListAsync();
            foreach (var item in list)
            {
                await _productsService.CancelOrder(item.ProductID, item.Vol);
            }
        }

        private async Task<VMOrderDetail> GetVMOrderDetailByOrderObject(Order order, List<OrderDetail> orderDetail)
        {
            VMOrderDetail vMOrderDetail = new VMOrderDetail();
            vMOrderDetail.OrderDate = order.OrderDate;
            vMOrderDetail.ShippingDate = order.ShippingDate;
            vMOrderDetail.sendWay = order.ShippingWay.ShippingWayName;
            vMOrderDetail.ShippingAddr = order.ShippingAddr;
            vMOrderDetail.OrderStatusID = order.Ordertatus.OrdertatusName;
            vMOrderDetail.OrderPhone = order.OrderPhone;
            vMOrderDetail.OrderName = order.OrderName;
            if (order.HandleOrder != null && order.HandleOrder.Count > 0)
            {
                vMOrderDetail.HandleMember = order.HandleOrder[order.HandleOrder.Count - 1].Staff.Name;
                vMOrderDetail.HandleDate = order.HandleOrder[order.HandleOrder.Count - 1].HandleTime;
            }
            if (order.Invoice != null)
            {
                vMOrderDetail.InvoiceID = order.Invoice.InvoiceNo;
            }    
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
