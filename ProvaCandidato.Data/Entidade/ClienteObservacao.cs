using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProvaCandidato.Data.Entidade
{
    public class ClienteObservacao
    {
        [Key]
        public int Codigo { get; set; }
        public string Observacao { get; set; }
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }
    }
}