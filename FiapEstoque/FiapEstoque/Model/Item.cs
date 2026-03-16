using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiapEstoque.Models
{
    [Table("T_Itens")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoUnitario { get; set; }

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;
    }
}