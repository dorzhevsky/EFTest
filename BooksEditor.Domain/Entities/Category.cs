using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksEditor.Domain.Entities
{
    [Table("categories")]
    public class Category: EntityBase
    {
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string CategoryDescription { get; set; }

        [InverseProperty("Category")]      
        public List<Product>  Products { get; set; }

        [NotMapped]
        public int Count { get; set; }
    }
}