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
    public class VMStaffsController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly StaffServices _staffServices;


        public VMStaffsController(GuestModelContext context, StaffServices staffServices)
        {
            _context = context;
            _staffServices = staffServices;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(StaffAcc login)
        {
            //var result = await _context.Login.Where(m => m.Account == login.Account && m.Password == login.Password).FirstOrDefaultAsync();
            // 直接寫資料庫語法
            //string sql = "Select * from Login where Account = '" + login.Account + "'and Password = '" + login.Password + "'";

            var result = await _staffServices.CheckMemberAcc(login);

            if (result != null)
            {
                //登入完成後須發給證明，證明他已登入
                //使用Session來當全域變數，紀錄登入狀態，須在Program.cs裡面註冊result.ToJson()
                HttpContext.Session.SetString("Admin", result.Account);
                var name = await _staffServices.GetNameByAccount(result.Account);
                HttpContext.Session.SetString("UserName", name);
                
                return RedirectToAction("Index", "ADProducts");
            }
            else
            {
                ViewData["Message"] = "帳號或密碼錯誤";
            }
            return View(login);
        }

        // GET: VMStaffs
        public async Task<IActionResult> Index()
        {
            var guestModelContext = _context.Staff.Include(s => s.Role);
            return View(await guestModelContext.ToListAsync());
        }

        // GET: VMStaffs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Role)
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        public IActionResult CreateAcc(string id)
        {
            ViewData["StaffID"] = id;
            return View();
        }

        // POST: VMStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcc([Bind("Account,Mima,StaffID")] StaffAcc staffAcc)
        {
            if (await _staffServices.CheckTheSameAcc(staffAcc.Account))
            {
                ViewData["ErrMsg"] = "已有相同帳號";
            }

            if (ModelState.IsValid)
            {
                await _staffServices.SaveStaffAcc(staffAcc);
                return RedirectToAction(nameof(Login));
            }
            return View(staffAcc);
        }

        // GET: VMStaffs/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleName");
            return View();
        }

        // POST: VMStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffID,Name,ArrivalDate,Phone,RoleID")] Staff staff)
        {
            staff.StaffID = await _staffServices.HandleStaffID(staff);
            ModelState.Clear(); // 清除舊的 ModelState
            TryValidateModel(staff); // 重新執行 Model 驗證
            if (ModelState.IsValid)
            {
                await _staffServices.SaveStaff(staff);
                return RedirectToAction(nameof(CreateAcc), new { id = staff.StaffID});
            }
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleID", staff.RoleID);
            return View(staff);
        }

        // GET: VMStaffs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleID", staff.RoleID);
            return View(staff);
        }

        // POST: VMStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StaffID,Name,ArrivalDate,Phone,RoleID")] Staff staff)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffID))
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
            ViewData["RoleID"] = new SelectList(_context.Role, "RoleID", "RoleID", staff.RoleID);
            return View(staff);
        }

        // GET: VMStaffs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Role)
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: VMStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(string id)
        {
            return _context.Staff.Any(e => e.StaffID == id);
        }
    }
}
