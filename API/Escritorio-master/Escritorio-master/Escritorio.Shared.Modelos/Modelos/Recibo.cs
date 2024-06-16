using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Modelos.Modelos
{
    public class Recibo
    {
        public Recibo() { }
        
        public Recibo(string referencia, decimal valor, string status,
            string comprovante, int propriedadeId)
        {
            Referencia = referencia;
            Valor = valor;
            Status = status;
            Comprovante = comprovante;
            PropriedadeId = propriedadeId;
        }
        [Key]
        public int Id { get; set; }
        public string Referencia { get; set; }
        public decimal? Valor { get; set; }
        public string? Status { get; set; }
        public string? Comprovante { get; set; }
        //chave estrangeira de recibo
        [ForeignKey("Propriedade")]
        public int? PropriedadeId { get; set; }

        // Propriedade de navegação para Endereco    
        public virtual Propriedade Propriedade { get; set; }

    }
}
