using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Core.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool Status { get; set; }
    }
}
