using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("nome")]
        [StringLength(50)]
        [Required]
        [MinLength(3, ErrorMessage = "A Cidade deve possui no minimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "A Cidade deve possui no maximo 50 caracteres")]
        [DisplayName("Cidade")]
        public string Nome { get; set; }
    }
}
