using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Core.Entities
{
    public class StoreHouse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool Status { get; set; }
    }
}
