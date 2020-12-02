using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIAADataModels.Transfer
{	public class Product
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Summary { get; set; }

		public List<string> ImageUrls { get; set; }

		public List<ProductAdditionalInformation> AdditionalInformation { get; set; }

		public List<ProductProperty> Properties { get; set; }		

		public List<ProductVariant> Variants { get; set; }

		public List<string> SkinConcerns { get; set; }

		public string LevelType { get; set; }

		public string Level { get; set; }
		
	}
}
