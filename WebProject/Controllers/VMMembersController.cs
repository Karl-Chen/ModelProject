using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Services;
using WebProject.ViewModels;
using static WebProject.WorkFunction.MimaHandler;

namespace WebProject.Controllers
{
    public class VMMembersController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly MemberServices _memberServices;
        public VMMembersController(GuestModelContext context, MemberServices memberServices)
        {
            _context = context;
            _memberServices = memberServices;
        }

        // GET: VMMembers1
        //public async Task<IActionResult> Index()
        //{
            
        //    return View(await _context.VMMembers.ToListAsync());
        //}

        // GET: VMMembers1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VMMembers vMMembers = await _memberServices.GetVMMemberByAccount(id);
            
            return View(vMMembers);
        }

        // GET: VMMembers1/Create
        public IActionResult Create(string uAction, string rRoute, string pid)
        {
            ViewData["uAction"] = uAction;
            ViewData["rRoute"] = rRoute;
            ViewData["pid"] = pid;
            return View();
        }


        // POST: VMMembers1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Account,Mima,reMima,Name,Address,Email,TelNumber")] VMMembers vMMembers, string? uAction, string? rRoute, string? pid)
        {
            if (vMMembers == null)
            {
                return BadRequest();
            }
            if (vMMembers.Mima != vMMembers.reMima)
            {
                ViewData["ErrMsg"] = "所輸入的密碼與確認密碼不同";
                return View(vMMembers);
            }
            string mid = await _memberServices.CreateNewMemberID();
            MemberAcc memberAcc = _memberServices.VMMMemberToMemberAcc(vMMembers, mid);
            Member member = _memberServices.VMMMemberToMember(vMMembers, mid);
            MemberTel memberTel = _memberServices.VMMMemberToMemberTel(vMMembers, mid);
            

            if (ModelState.IsValid)
            {
                await _memberServices.SaveMmeberGroup(member, memberAcc, memberTel);
                if (uAction != "" && uAction != null)
                {
                    string id = pid;
                    return RedirectToAction("login", "login", new { uAction = uAction, uRoute = rRoute, pid = id});
                    //return RedirectToAction(rRoute, uAction, new { id = pid });
                }
                return RedirectToAction("login", "login");
            }
            return View(vMMembers);

        }

        //GET: VMMembers1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vMMembers = await _memberServices.GetVMMemberByAccount(id);
            if (vMMembers == null)
            {
                return NotFound();
            }
            return View(vMMembers);
        }

        // POST: VMMembers1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Account,Mima,reMima,Name,Address,Email,TelNumber")] VMMembers vMMembers)
        {
            if (id != vMMembers.Account)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_memberServices.EditMemberGroup(vMMembers, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _memberServices.VMMembersExists(vMMembers.Account))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vMMembers);
        }

        //// GET: VMMembers1/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var vMMembers = await _context.VMMembers
        //        .FirstOrDefaultAsync(m => m.Account == id);
        //    if (vMMembers == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(vMMembers);
        //}

        //// POST: VMMembers1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var vMMembers = await _context.VMMembers.FindAsync(id);
        //    if (vMMembers != null)
        //    {
        //        _context.VMMembers.Remove(vMMembers);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        
    }
}
