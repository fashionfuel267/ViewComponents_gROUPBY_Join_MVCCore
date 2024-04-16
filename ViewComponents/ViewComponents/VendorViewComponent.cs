using Microsoft.AspNetCore.Mvc;
using ViewComponents.Models;

namespace ViewComponents.ViewComponents
{
    public class VendorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var db = new APContext();
            var model = db.Vendors.OrderBy(v => v.VendorState).Select(s=> s.VendorState).Distinct().ToList();
            return View(model);
        }

    }
}
