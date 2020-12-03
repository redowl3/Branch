using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIAADataModels.Transfer
{	
	public class Salon
	{
		public Guid Id { get; set; }
		public string Name { get; set; }	
		
		public List<ProductCategory> Products { get; set; }

	}
}
