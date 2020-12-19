using System;
using System.Collections.Generic;
namespace IIAADataModels.Transfer
{
    public class Salon
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Survey> Surveys { get; set; }
        public object Therapists { get; set; }

    }
}
