using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Numero de serie")]
        public string SerialNumber { get; set; }

        [Required]
        [MaxLength(60)]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Required]
        [Range(1, 100000)]
        [Display(Name = "Costo")]
        public double Cost { get; set; }

        public string ImageUrl { get; set; }

        // Foreign keys

        [Required]
        public int IdCategory { get; set; }

        [ForeignKey("IdCategory")]
        public Category Category { get; set; }

        [Required]
        public int IdBrand { get; set; }

        [ForeignKey("IdBrand")]
        public Brand Brand { get; set; }

        // Recursivity

        public int? BaseId { get; set; }
        public virtual Product Base { get; set; }
    }
}
