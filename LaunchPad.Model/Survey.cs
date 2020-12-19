using System;
using System.Collections.Generic;

namespace IIAADataModels.Transfer
{
    public class Survey
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<Page> Pages { get; set; }
    }
}