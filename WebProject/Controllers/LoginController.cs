using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebProject.Models;
using WebProject.Services;

namespace WebProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly GuestModelContext _context;
        private readonly MemberServices _memberServices;
        private string mAction, mRoute, mID;
        public LoginController(GuestModelContext context, MemberServices memberServices)
        {
            _context = context;
            _memberServices = memberServices;
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID, Account, Mima")] MemberAcc memberAcc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberAcc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberAcc);
        }

        public IActionResult Login(string uAction, string uRoute, string pid)
        {
            mAction = uAction;
            mRoute = uRoute;
            mID = pid;
            ViewData["action"] = uAction;
            ViewData["uRoute"] = uRoute;
            ViewData["id"] = pid;
            return View();
        }

        //model001/123Model ; model002/456Model

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberAcc login, bool Keep, string? uAction, string? rRoute, string? pid)
        {
            //var result = await _context.Login.Where(m => m.Account == login.Account && m.Password == login.Password).FirstOrDefaultAsync();
            // 直接寫資料庫語法
            //string sql = "Select * from Login where Account = '" + login.Account + "'and Password = '" + login.Password + "'";

            var result = await _memberServices.CheckMemberAcc(login);


            if (result != null)
            {
                //登入完成後須發給證明，證明他已登入
                //使用Session來當全域變數，紀錄登入狀態，須在Program.cs裡面註冊result.ToJson()
                HttpContext.Session.SetString("Manager", result.Account);
                var memberID = await _memberServices.GetMemberIDByAccount(result.Account);
                string name = "";
                if (memberID != null)
                {
                    name = await _memberServices.GetNameByMemberID(memberID);
                }
                HttpContext.Session.SetString("UserName", name);
                string strkeep = Keep.ToString();
                HttpContext.Session.SetString("Keep", strkeep);
                int keepday = 30;
                
                Response.Cookies.Append("ManagerCookie", result.Account, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(keepday)
                });
                Response.Cookies.Append("KeepCookie", strkeep, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(keepday)
                });

                if (uAction != "" && uAction != null)
                    return RedirectToAction(rRoute, uAction, new { id = pid });
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewData["Message"] = "帳號或密碼錯誤";
            }
            return View(login);
        }

        public IActionResult Logout()
        {
            //5.4.1 在LoginController加入Logout Action
            HttpContext.Session.Remove("Manager");
            Response.Cookies.Delete("ManagerCookie");
            return RedirectToAction("Index", "Home");
        }

        
    }
}
