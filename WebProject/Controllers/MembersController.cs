using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Services;

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

        public async Task<IActionResult> UserDetails()
        {
            var id = HttpContext.Session.GetString("Manager");
            if (id == null)
            {
                return NotFound();
            }
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

        
        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("MemberID,Name,Address,Email")] Member member)
        //{
        //    if (id != member.MemberID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(member);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MemberExists(member.MemberID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(member);
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
