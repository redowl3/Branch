using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIAADataModels.Transfer.Base
{
	public class ProductBase
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Summary { get; set; }

		public string Classification { get; set; }
		public string LevelType { get; set; }

		public string Level { get; set; }
		public List<ProductVariant> Variants { get; set; }

		public decimal LowestPrice()
		{
			return this.Variants.Min(e => e.Price);
		}

		public bool HasOffers()
		{
			return this.Variants.Any(e => e.Price < e.RRP);			
		}
	}
}
