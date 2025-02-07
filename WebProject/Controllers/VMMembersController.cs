using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.ViewModels;
using static WebProject.WorkFunction.MimaHandler;

namespace WebProject.Controllers
{
    public class VMMembersController : Controller
    {
        private readonly GuestModelContext _context;

        public VMMembersController(GuestModelContext context)
        {
            _context = context;
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
            VMMembers vMMembers = new VMMembers();
            var memberAcc = await _context.MemberAcc
                .FirstOrDefaultAsync(m => m.Account == id);
            if (memberAcc == null)
            {
                return NotFound();
            }
            var member = await _context.Member.FirstOrDefaultAsync(m => m.MemberID == memberAcc.MemberID);
            if (member == null)
            {
                return NotFound();
            }
            var memberTel = await _context.MemberTel.FirstOrDefaultAsync(m => m.MemberID == memberAcc.MemberID);
            if (memberTel == null)
            {
                return NotFound();
            }
            vMMembers.Account = memberAcc.Account;
            vMMembers.Mima = memberAcc.Mima;
            vMMembers.reMima = memberAcc.Mima;
            vMMembers.Name = member.Name;
            vMMembers.Email = member.Email;
            vMMembers.Address = member.Address;
            vMMembers.TelNumber = memberTel.TelNumber;

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
            MemberAcc memberAcc = new MemberAcc();
            Member member = new Member();
            MemberTel memberTel = new MemberTel();
            memberAcc.Account = vMMembers.Account;
            if (vMMembers == null)
            {
                return BadRequest();
            }
            if (vMMembers.Mima != vMMembers.reMima)
            {
                ViewData["ErrMsg"] = "所輸入的密碼與確認密碼不同";
                return View(vMMembers);
            }
            var mima = Get_SHA256_Hash(vMMembers.Mima);
            memberAcc.Mima = mima;
            string mid = "M00001";
            var memberID = await _context.Member.OrderByDescending(c => c.MemberID).Select(c => c.MemberID).FirstOrDefaultAsync();
            if (memberID != null && memberID.Length > 0)
            {
                mid = memberID.Substring(1, memberID.Length - 1);
                int intMid = int.Parse(mid);
                intMid++;
                mid = "M" + intMid.ToString().PadLeft(5, '0');
            }
            memberAcc.MemberID = mid;
            member.MemberID = mid;
            member.Name = vMMembers.Name;
            member.Address = vMMembers.Address;
            member.Email = vMMembers.Email;
            memberTel.TelNumber = vMMembers.TelNumber;
            memberTel.MemberID = mid;

            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                _context.Add(memberAcc);
                await _context.SaveChangesAsync();
                _context.Add(memberTel);
                await _context.SaveChangesAsync();

                
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
            var memberAcc = await _context.MemberAcc.FindAsync(id);
            var member = await _context.Member.FindAsync(memberAcc.MemberID);
            var memberTel = await _context.MemberTel.FindAsync(memberAcc.MemberID);
            if (member == null || memberAcc == null || memberTel == null)
                return NotFound();
            var vMMembers = MemberToVMMember(member, memberAcc, memberTel);
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
                    var member = VMMMemberToMember(vMMembers);
                    var memberAcc = VMMMemberToMemberAcc(vMMembers);
                    var memberTel = VMMMemberToMemberTel(vMMembers);
                    var memberid = await _context.MemberAcc.Where(c => c.Account == id).Select(c => c.MemberID).FirstOrDefaultAsync();
                    member.MemberID = memberid;
                    memberAcc.MemberID = memberid;
                    memberTel.MemberID = memberid;
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                    _context.Update(memberAcc);
                    await _context.SaveChangesAsync();
                    _context.Update(memberTel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VMMembersExists(vMMembers.Account))
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

        private bool VMMembersExists(string id)
        {
            return _context.MemberAcc.Any(e => e.Account == id);
        }

        private Member VMMMemberToMember(VMMembers vMMembers)
        {
            Member member = new Member();
            member.Address = vMMembers.Address;
            member.Name = vMMembers.Name;
            member.Email = vMMembers.Email;
            return member;
        }
        private MemberAcc VMMMemberToMemberAcc(VMMembers vMMembers)
        {
            MemberAcc memberAcc = new MemberAcc();
            memberAcc.Account = vMMembers.Account;
            memberAcc.Mima = vMMembers.Mima;
            
            return memberAcc;
        }

        private MemberTel VMMMemberToMemberTel(VMMembers vMMembers)
        {
            MemberTel memberTel = new MemberTel();
            memberTel.TelNumber = vMMembers.TelNumber;

            return memberTel;
        }

        private VMMembers MemberToVMMember(Member member, MemberAcc memberacc, MemberTel membertel)
        {
            VMMembers vMMembers = new VMMembers();
            vMMembers.Mima = memberacc.Mima;
            vMMembers.Account = memberacc.Account;
            vMMembers.Address = member.Address;
            vMMembers.TelNumber = membertel.TelNumber;
            vMMembers.Email = member.Email;
            vMMembers.Name = member.Name;
            return vMMembers;
        }
    }
}
