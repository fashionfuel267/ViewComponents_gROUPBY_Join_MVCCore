namespace ViewComponents.ViewModels
{
    public class VendorInvoiceVM
    {
        public string VendorName {  get; set; }
		public string VendorState { get; set; }
		public string VendorAddress {  get; set; }
        public decimal PaymentTotal {  get; set; }
        public decimal InvoiceTotal {  get; set; }
        public decimal TotalDue {  get; set; }
		public decimal TotalInvoice { get; set; }
	}
}
