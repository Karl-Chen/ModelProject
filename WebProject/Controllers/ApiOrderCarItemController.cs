using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebProject.Models;
using WebProject.Services;
using WebProject.ViewModels;
using WebProject.WorkFunction;

namespace WebProject.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ApiOrderCarItemController : ControllerBase
    {
        private readonly GuestModelContext _context;
        private readonly OrderCarServices _orderCarServices;
        private readonly OrderServices _orderServices;
        private readonly OrderDetailService _orderDetailService;
        private readonly MemberServices _memberService;
        private readonly FileIOFunction _fileIOFunction;

        public ApiOrderCarItemController(GuestModelContext context, OrderCarServices orderCarServices, OrderServices orderServices, OrderDetailService orderDetailService, MemberServices memberService, FileIOFunction fileIOFunction)
        {
            _context = context;
            _orderCarServices = orderCarServices;
            _orderServices = orderServices;
            _orderDetailService = orderDetailService;
            _memberService = memberService;
            _fileIOFunction = fileIOFunction;
        }

        [HttpGet("GetOrderCarData/{acc}")]
        public async Task<ActionResult<VMOrderCar>> GetOrderCarData(string acc)
        {
            acc += ".txt";
            VMOrderCar vMOrderCar = await _orderCarServices.GetOrderCarList(acc);
            if (vMOrderCar == null)
            {
                return NotFound("您的購物車尚無資料");
            }
            return vMOrderCar;
        }
        [HttpPut("PutOrderCarData/{acc}")]
        public async Task<IActionResult> PutOrderCarData(string acc, [FromBody] List<VMSimpleOrderCarItem> list)
        {
            if (acc == null)
                return BadRequest("帳號不得為空");

            var oldMember = await _memberService.GetMemberbyAcc(acc);
            if (oldMember == null)
                return NotFound("查無此會員購物車資料");

            await _orderCarServices.HandleOrderCarAllList(acc, list);

            return NotFound("購物車更新成功！");
        }



        [HttpPut("{acc}")]
        public async Task<IActionResult> PutOrderCarItem(string acc, [FromBody] VMSimpleOrderCarItem item)
        {
            if (item is null)
            {
                return BadRequest("購物車項目不得為空");
            }

            var oldMember = await _memberService.GetMemberbyAcc(acc);
            if (oldMember == null)
                return NotFound("查無此會員購物車資料");

            string fileName = acc + ".txt";
            List<string> orderList = _fileIOFunction.ReadFileContent(fileName);
            bool isSame = _orderCarServices.CheckSameItem(fileName, item.productID, item.count, orderList);

            if (!isSame)
            {
                string orderdetail = item.productID + "," + item.count;
                _fileIOFunction.WriteFileAppend(fileName, orderdetail);
            }
            else
            {
                _fileIOFunction.WriteFileOverWrite(fileName, orderList);
            }

            return Ok("商品已加進購物車！");
        }

        [HttpGet("GetOrderList")]
        public async Task<ActionResult<List<Order>>> GetOrderList(string acc)
        {
            List<Order> orderList = await _orderServices.GetOrderListByAcc(acc);
            if (orderList == null)
            {
                return NotFound("您還沒有成立的訂單");
            }
            return await _orderServices.GetOrderListByAcc(acc);
        }

        [HttpPut("CancelOrder/{orderNo}/{userId}")]
        public async Task<ActionResult<List<Order>>> CancelOrder(string orderNo, string userId)
        {
            Order order = await _orderServices.GetOrderByOrderNo(orderNo);
            if (order == null)
            {
                return NotFound("查無此訂單(" + orderNo + ")");
            }
            //order.Ordertatus = "10"
            await _orderServices.CancelOrder(orderNo);
            return await _orderServices.GetOrderListByAcc(userId);
        }

        [HttpPost("PostOrder/{acc}/{OrderName}/{OrderPhone}")]
        public async Task<ActionResult> PostOrder(string acc, string OrderName, string OrderPhone, [FromBody] VMOrderCar vMOrderCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            acc += ".txt";
            await _fileIOFunction.WriteFileOverWrite(acc, vMOrderCar);
            var orderNo = await _orderServices.WriteToOrderTable(vMOrderCar.sendWay, vMOrderCar.isFix, acc, OrderPhone, OrderName);
            await _orderDetailService.WriteToOrderDetailTable(orderNo, vMOrderCar);
            return Ok("訂單成立成功！");
        }


        [HttpGet("GetOrderDetailList")]
        public async Task<List<OrderDetail>> GetOrderDetailList(string orderNo)
        {
            return await _orderDetailService.GetOrderDetailByOrderNo(orderNo);
        }


        private bool OrderDetailExists(string id)
        {
            return _context.OrderDetail.Any(e => e.OrderNo == id);
        }
    }
}
