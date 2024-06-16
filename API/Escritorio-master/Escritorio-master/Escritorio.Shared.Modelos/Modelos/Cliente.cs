using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Modelos.Modelos
{
    public class Cliente
    {
        public Cliente() 
        {
            Propriedades = new List<Propriedade>();
        }
        public Cliente(string nome, string cpf, string rg, string celular, int enderecoId)
        {
            Nome = nome;
            CPF = cpf;
            RG = rg;
            Celular = celular;
            EnderecoId = enderecoId;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
        public string? Celular { get; set; }
        //chave estrangeira de endereco
        [ForeignKey("Endereco")]
        public int EnderecoId { get; set; }
        //propriedade de navegação para endereco
        public virtual Endereco Endereco { get; set; }

        // Lista de propriedades associadas a este cliente
        public virtual List<Propriedade> Propriedades { get; set; }
    }
}
