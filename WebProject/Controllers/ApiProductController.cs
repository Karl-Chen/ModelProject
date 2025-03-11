using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Collections.Generic;
using WebProject.Models;
using WebProject.Services;
using WebProject.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebProject.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ApiProductController : ControllerBase
    {
        private readonly ProductsService _productService;
        private readonly OrderCarServices _orderCarServices;
        private readonly OrderServices _orderServices;
        private readonly OrderDetailService _orderDetailService;

        public ApiProductController(ProductsService productService, OrderCarServices orderCarServices, OrderServices orderServices, OrderDetailService orderDetailService)
        {
            _productService = productService;
            _orderCarServices = orderCarServices;
            _orderServices = orderServices;
            _orderDetailService = orderDetailService;
        }

        // GET: api/<ApiProductController>
        [HttpGet("GetProductList")]
        public async Task<List<Product>> GetProductList(string type, bool isAll)
        {
            var productList = await _productService.GetProductListDetailListByPSID(type, isAll);
            return productList;
        }

        [HttpGet("GetProductSpecification")]
        public async Task<List<ProductSpecification>> GetProductSpecification()
        {
            List<ProductSpecification> productSpecificationList = await _productService.GetProductSpecificationList();
            return productSpecificationList;
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

        //[HttpPut("PutOrderCarData")]
        //public async Task<IActionResult> PutOrderCarData([FromBody] MemberAcc memberAcc)
        //{
        //    if (acc == null)
        //        return BadRequest();

        //    var oldMember = await _memberService.GetMemberbyAcc(acc);
        //    if (oldMember == null)
        //        return NotFound("查無此會員資料");

        //    MemberAcc ret;
        //    if (ModelState.IsValid)
        //    {
        //        ret = await _memberService.UpdateMemberAcc(memberAcc);
        //        return Ok(ret);
        //    }

        //    return NotFound("會員密碼更新失敗！");
        //}

        //[HttpPut("PutOrderCarItem")]
        //public async Task<IActionResult> PutOrderCarItem([FromBody] VMSimpleOrderCarItem item)
        //{
           
        //    var oldMember = await _memberService.GetMemberbyAcc(acc);
        //    if (oldMember == null)
        //        return NotFound("查無此會員資料");

        //    MemberAcc ret;
        //    if (ModelState.IsValid)
        //    {
        //        ret = await _memberService.UpdateMemberAcc(memberAcc);
        //        return Ok(ret);
        //    }

        //    return NotFound("會員密碼更新失敗！");
        //}

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("PostOrderCarData")]
        //public async Task<ActionResult<Member>> PostMember([FromBody] VMMembers vmMember)
        //{
        //    var oldMember = await _memberService.GetMemberbyAcc(vmMember.Account);
        //    if (oldMember != null)
        //        return NotFound("此帳號被註冊過了");


        //    string mid = await _memberService.CreateNewMemberID();
        //    MemberAcc memberAcc = _memberService.VMMMemberToMemberAcc(vmMember, mid);
        //    Member member = _memberService.VMMMemberToMember(vmMember, mid);
        //    MemberTel memberTel = _memberService.VMMMemberToMemberTel(vmMember, mid);
        //    await _memberService.SaveMmeberGroup(member, memberAcc, memberTel);


        //    return CreatedAtAction("GetMemberInfo", new { acc = memberAcc.Account, mima = memberAcc.Mima }, member);
        //}



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

        [HttpGet("GetOrderDetailList")]
        public async Task<List<OrderDetail>> GetOrderDetailList(string orderNo)
        {
            return await _orderDetailService.GetOrderDetailByOrderNo(orderNo);
        }

        

    }
}
