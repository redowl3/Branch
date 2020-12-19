using System.Collections.Generic;
namespace IIAADataModels.Transfer
{
    public class ProductVariant
	{
		public string Name { get; set; }
		public string Detail { get; set; }
		public int Quantity { get; set; }
		public string Unit { get; set; }
		public decimal Price { get; set; }
		public int LoyaltyPoints { get; set; }
		public string Barcode { get; set; }
		public List<PrescribingOption> PrescribingOptions { get; set; }
        public ProductVariant()
        {
			PrescribingOptions = new List<PrescribingOption>();
        }
	}

	public class PrescribingOption
	{
		public string Id { get; set; }
		public string Title { get; set; }
	}
}
