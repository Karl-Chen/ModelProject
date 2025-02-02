using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly GuestModelContext _context;
        public LoginController(GuestModelContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberAcc login)
        {
            //var result = await _context.Login.Where(m => m.Account == login.Account && m.Password == login.Password).FirstOrDefaultAsync();
            // 直接寫資料庫語法
            //string sql = "Select * from Login where Account = '" + login.Account + "'and Password = '" + login.Password + "'";
            string sql = "Select * from Login where Account = @Account and Password = @Password";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Account", login.Account),
                    new SqlParameter("@Password", login.Mima)
                };
            var result = await _context.MemberAcc.FromSqlRaw(sql, parameters).FirstOrDefaultAsync();


            if (result != null)
            {
                //登入完成後須發給證明，證明他已登入
                //使用Session來當全域變數，紀錄登入狀態，須在Program.cs裡面註冊result.ToJson()
                HttpContext.Session.SetString("Manager", result.Account);

                return RedirectToAction("Index", "BooksManage");
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
            return RedirectToAction("Index", "Home");
        }
    }
}
