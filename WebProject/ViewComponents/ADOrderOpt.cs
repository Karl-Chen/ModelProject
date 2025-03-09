using Microsoft.AspNetCore.Mvc;

namespace WebProject.ViewComponents
{
    public class ADOrderOpt : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
