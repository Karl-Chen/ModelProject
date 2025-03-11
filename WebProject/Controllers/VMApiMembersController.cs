using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Services;
using WebProject.ViewModels;

namespace WebProject.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class VMApiMembersController : ControllerBase
    {
        private readonly GuestModelContext _context;
        private readonly MemberServices _memberService;

        public VMApiMembersController(GuestModelContext context, MemberServices memberService)
        {
            _context = context;
            _memberService = memberService;
        }

        [HttpGet("{acc}")]
        public async Task<Member> GetMemberInfoByAcc(string acc)
        {
            return await _memberService.GetMemberbyAcc(acc);
        }


        [HttpGet("GetMemberInfo")]
        public async Task<ActionResult<Member>> GetMemberInfo(string acc, string mima)
        {
            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(mima))
                return BadRequest("帳號與密碼不能為空");

            MemberAcc memberAcc = new MemberAcc();
            memberAcc.Account = acc;
            memberAcc.Mima = mima;
            var result = await _memberService.CheckMemberAcc(memberAcc);

            if (result == null)
            {
                return NotFound("帳號或密碼錯誤!");
            }

            return await _memberService.GetMemberbyAcc(acc);
        }

        [HttpPut("{acc}")]
        public async Task<IActionResult> PutMember(string acc, [FromBody] Member member)
        {
            if (string.IsNullOrEmpty(acc))
                return BadRequest("會員帳號不能為空");
            if (member == null)
                return BadRequest("請提供會員資料");
            var memberName = await _memberService.GetMemberbyAcc(acc);
            if (memberName == null)
                return NotFound("查無此會員資料");

            Member ret;
            if (ModelState.IsValid)
            {
                ret = await _memberService.UpdateMember(member);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("PutMemberAcc")]
        public async Task<IActionResult> PutMemberAcc([FromBody] MemberAcc memberAcc)
        {
            if (string.IsNullOrEmpty(memberAcc.Account))
                return BadRequest("帳號不能為空");
            var oldMember = await _memberService.GetMemberbyAcc(memberAcc.Account);
            if (oldMember == null)
                return NotFound("查無此會員資料");

            MemberAcc ret;
            if (ModelState.IsValid)
            {
                ret = await _memberService.UpdateMemberAcc(memberAcc);
                return Ok(ret);
            }

            return BadRequest(ModelState);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostMember")]
        public async Task<ActionResult<Member>> PostMember([FromBody] VMMembers vmMember)
        {
            var oldMember = await _memberService.GetMemberbyAcc(vmMember.Account);
            if (oldMember != null)
                return Conflict("此帳號被註冊過了");


            string mid = await _memberService.CreateNewMemberID();
            MemberAcc memberAcc = _memberService.VMMMemberToMemberAcc(vmMember, mid);
            Member member = _memberService.VMMMemberToMember(vmMember, mid);
            MemberTel memberTel = _memberService.VMMMemberToMemberTel(vmMember, mid);
            await _memberService.SaveMmeberGroup(member, memberAcc, memberTel);


            return CreatedAtAction(nameof(GetMemberInfoByAcc), new { acc = memberAcc.Account});
        }


        private bool VMMembersExists(string id)
        {
            return _context.VMMembers.Any(e => e.Account == id);
        }
    }
}
