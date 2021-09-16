using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("codigo")]
        [DisplayName("#")]
        public int Codigo { get; set; }

        [StringLength(50)]
        [Required]
        [Column("nome")]
        public string Nome { get; set; }
        [Column("data_nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data de Nascimento")]

        public DateTime? DataNascimento { get; set; }

        [Column("codigo_cidade")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("CidadeId")]
        [DisplayName("Cidades")]
        public virtual Cidade Cidade { get; set; }

        public ICollection<ClienteObservacao> Observacoes { get; set; }

    }
}