﻿using System;
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

        [HttpGet("GetOrderCarData")]
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
                return NotFound("查無此會員資料");

            MemberAcc ret;
            if (ModelState.IsValid)
            {
                ret = await _memberService.UpdateMemberAcc(memberAcc);
                return Ok(ret);
            }

            return NotFound("會員密碼更新失敗！");
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
            bool isSame = _orderCarServices.CheckSameItem(fileName, item.productID, item.value, orderList);

            if (!isSame)
            {
                string orderdetail = item.productID + "," + item.value;
                _fileIOFunction.WriteFileAppend(fileName, orderdetail);
            }
            else
            {
                _fileIOFunction.WriteFileOverWrite(fileName, orderList);
            }

            

            return Ok("會員密碼更新失敗！");
        }


        // PUT: api/ApiOrderCarItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(string id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderNo)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiOrderCarItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetail.Add(orderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDetail.OrderNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderNo }, orderDetail);
        }

        // DELETE: api/ApiOrderCarItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(string id)
        {
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetail.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(string id)
        {
            return _context.OrderDetail.Any(e => e.OrderNo == id);
        }
    }
}
