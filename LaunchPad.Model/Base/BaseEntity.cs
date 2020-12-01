using System;
using System.ComponentModel.DataAnnotations;

namespace LaunchPad.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime datestamp { get; set; }
        public string status { get; set; }
    }
}
