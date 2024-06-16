using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Modelos.Modelos
{
    public class Endereco
    {
        public Endereco() { }
        public Endereco(string rua, string bairro, string cep, int cidadeId)
        {
            Rua = rua;
            Bairro = bairro;
            CEP = cep;
            CidadeId = cidadeId;
        }
        [Key]
        public int Id { get; set; }
        public string Rua { get; set; }
        public string? Bairro { get; set; }
        public string? CEP { get; set; }
        //Chave estrangeira de Cidade
        [ForeignKey("Cidade")]
        public int CidadeId { get; set; }

        // Propriedade de navegação para Cidade
        public virtual Cidade Cidade { get; set; }
    }
}
