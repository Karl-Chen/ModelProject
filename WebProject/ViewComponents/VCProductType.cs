using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Services;
using WebProject.WorkFunction;

namespace WebProject.ViewComponents
{
    public class VCProductType:ViewComponent
    {
        private readonly GuestModelContext _context;
        private readonly FileIOFunction _fileIOFunction;
        private readonly MemberServices _memberServices;
        public VCProductType(GuestModelContext context, FileIOFunction fileIOFunction, MemberServices memberServices    )
        {
            _context = context;
            _fileIOFunction = fileIOFunction;
            _memberServices = memberServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //因為須要早點去取得使用者名稱及購物車資訊，所以把程式碼移來這裡
            string acc = GetOrderCarCount();
            string name = await _memberServices.GetNameByAccount(acc);
            if (name != null && name != "")
                HttpContext.Session.SetString("UserName", name);

            var types = await _context.ProductSpecification.OrderBy(b => b.SpecificationName).ToArrayAsync();
            
            return View(types);
        }

        private string GetOrderCarCount()
        {
            var varacc = HttpContext.Session.GetString("Manager");
            if (varacc == null)
            {
                return null;
            }
            string account = varacc.ToString();
            string fileName = account + ".txt";
            int count = _fileIOFunction.ReadFileCount(fileName);
            HttpContext.Session.SetString("OrderCar", count.ToString());
            return varacc;
        }
    }
}
