using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Services;
using WebProject.ViewModels;

namespace WebProject.Controllers
{
    public class MembersController : Controller
    {
        //private readonly GuestModelContext _context;
        private readonly MemberServices _memberServices;

        public MembersController(MemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _memberServices.GetMemberList());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberServices.GetMemberbyAcc(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        public async Task<IActionResult> UserDetails(string? str)
        {
            var id = HttpContext.Session.GetString("Manager");
            if (id == null)
            {
                return NotFound();
            }
            if (str != null && str != "")
                ViewData["Message"] = str;
            var member = await _memberServices.GetVMMemberByAccount(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> EditMima(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var members = await _memberServices.GetVMEditMemberAccByAcc(id);
            if (members == null)
            {
                return NotFound();
            }
            return View(members);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMima([Bind("Account,Mima,reMima,OldMima, MemberID")] VMEditMemberAcc vMEditAcc)
        {
            if (!(await _memberServices.isOldMima(vMEditAcc)))
            {
                ViewData["ErrMsg"] = "您輸入的舊密碼與原本密碼不符！";
                return View(vMEditAcc);
            }
            if (vMEditAcc.Mima != vMEditAcc.reMima)
            {
                ViewData["ErrMsg"] = "您輸入的新密碼與再次輸入的密碼不符！";
                return View(vMEditAcc);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _memberServices.EditMemberAcc(vMEditAcc);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _memberServices.VMMembersExists(vMEditAcc.Account))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                string str = "密碼已更新!";
                return RedirectToAction(nameof(UserDetails), new { str});
            }
            return View(vMEditAcc);
        }

        public async Task<IActionResult> EditMember(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var members = await _memberServices.GetVMEditMemberByAcc(id);
            if (members == null)
            {
                return NotFound();
            }
            return View(members);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMember([Bind("MemberID,Name,Address,Email,TelNumber")] VMEditMember vMEditMember)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _memberServices.EditMember(vMEditMember);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                string str = "會員資料已更新!";
                return RedirectToAction(nameof(UserDetails), new { str });
            }
            return View(vMEditMember);
        }

            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> EditMember([Bind("MemberID,Name,Address,Email")] Member memeber)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            await _memberServices.EditMemberAcc(vMEditAcc);
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            throw;
            //        }
            //        string str = "會員資料已更新!";
            //        return RedirectToAction(nameof(UserDetails), new { str });
            //    }
            //    return View(memeber);
            //}

            //// GET: Members/Delete/5
            //public async Task<IActionResult> Delete(string id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var member = await _context.Member
            //        .FirstOrDefaultAsync(m => m.MemberID == id);
            //    if (member == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(member);
            //}

            //// POST: Members/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> DeleteConfirmed(string id)
            //{
            //    var member = await _context.Member.FindAsync(id);
            //    if (member != null)
            //    {
            //        _context.Member.Remove(member);
            //    }

            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //private bool MemberExists(string id)
            //{
            //    return _context.Member.Any(e => e.MemberID == id);
            //}
        }
}
