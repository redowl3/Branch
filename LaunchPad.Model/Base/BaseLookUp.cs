using System.ComponentModel.DataAnnotations;

namespace LaunchPad.Model.Base
{
    public class BaseLookUp
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

    }
}
