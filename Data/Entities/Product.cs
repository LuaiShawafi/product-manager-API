using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.Data.Entities
{
    [Index(nameof(Sku), IsUnique = true)]
    public class Product
    {

        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public int Sku { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string ImageURL { get; set; }
        [MaxLength(10)]
        public int Price { get; set; }
    }
}
