using Microsoft.AspNetCore.Mvc;
using ViewComponents.Models;
using ViewComponents.ViewModels;
using static NuGet.Packaging.PackagingConstants;

namespace ViewComponents.Controllers
{
    public class VendorController : Controller
    {
        private APContext context;
        public VendorController(APContext _context)
        {
            this.context = _context;
        }
        public IActionResult Vendor(string name)
        {
			
			if (name != null)
			{
				//var vendor = context.Vendors.Where(v => v.VendorState.ToLower().Equals(name.ToLower())).ToList();
				//var vendorList =vendor 
				//					.Join(context.Invoices, v => v.VendorId, i => i.VendorId, (v, i) =>
				//new { vndr = v, invc = i })
				//	.Select(s => new VendorInvoiceVM
				//{
				//	VendorName = s.vndr.VendorName,
				//	VendorAddress = $"{s.vndr.VendorAddress1 ?? s.vndr.VendorAddress2},{s.vndr.VendorState},{s.vndr.VendorCity},{s.vndr.VendorZipCode}",
				//	InvoiceTotal = s.invc.InvoiceTotal,
				//	PaymentTotal =  s.invc.PaymentTotal,
				//	TotalDue = s.invc.InvoiceTotal - s.invc.CreditTotal - s.invc.PaymentTotal
				//});
				var result = (from a in context.Invoices
							 join b in context.Vendors on a.VendorId equals b.VendorId into vinv
							 from vinv_v in vinv.DefaultIfEmpty()
							 group a by new {
                                 vinv_v.VendorId,
                                 vinv_v.VendorName,
                                 vinv_v.VendorAddress1,
                                 vinv_v.VendorAddress2,
                                 vinv_v.VendorCity,
                                 vinv_v.VendorState ,
                                 vinv_v.VendorZipCode} into c
							  select new VendorInvoiceVM
							 {
								  VendorState=c.Key.VendorState,
								 VendorName = c.Key.VendorName,
								 VendorAddress = $"{c.Key.VendorAddress1 ?? c.Key.VendorAddress2},{c.Key.VendorState},{c.Key.VendorCity},{c.Key.VendorZipCode}",
								 TotalInvoice = c.Count() ,
								  InvoiceTotal = c.Sum(p=>p.InvoiceTotal),
								 PaymentTotal = c.Sum(p=>p.PaymentTotal),
								 TotalDue = (c.Sum(p => p.InvoiceTotal))- (c.Sum(p => p.PaymentTotal))- (c.Sum(p => p.CreditTotal))
							 }).Where(p=>p.VendorState.Equals(name)) ;

				return View(result);
			}

			return View();

			
        }
       
    }
}
