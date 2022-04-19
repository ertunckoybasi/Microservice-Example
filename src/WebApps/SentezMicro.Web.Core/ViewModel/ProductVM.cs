namespace SentezMicro.Web.Core.ViewModel
{
    public class ProductVM
    {
        public ProductVM()
        {
            RecId = 0;
            Barcode = "XXXX212012XA";
            Price = 0;
            Description = "Ürün Açıklaması";
        }
        public int RecId { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
