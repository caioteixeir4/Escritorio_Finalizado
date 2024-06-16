using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Modelos.Modelos
{
    public class Propriedade
    {
        public Propriedade() 
        {
        }
        public Propriedade(string nome, string cnpj, string inscricaoEstadual,
            string status, int numPasta,
            int enderecoId, int clienteId)
        {
            Nome = nome;
            CNPJ = cnpj;
            InscricaoEstadual = inscricaoEstadual;
            Status = status;
            NumPasta = numPasta;
            EnderecoId = enderecoId;
            ClienteId = clienteId;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? Status { get; set; }
        public int NumPasta { get; set; }

        //chave estrangeira de endereco
        [ForeignKey("Endereco")]
        public int EnderecoId { get; set; }

        //chave estrangeira de cliente
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        // Propriedade de navegação para Endereco    
        public virtual Endereco Endereco { get; set; }

        // Propriedade de navegação para Cliente       
        public virtual Cliente Cliente { get; set; }

    }
}
