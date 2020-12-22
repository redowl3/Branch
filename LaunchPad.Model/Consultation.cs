using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIAADataModels.Transfer
{
	public class Consultation
	{
		//TODO
		public Guid Id { get; set; }

		public Basket HealthPlan { get; set; }
		public Basket Basket { get; set; }

		


	}
}
