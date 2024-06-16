using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Modelos.Modelos
{
    public class Cidade
    {
        public Cidade() 
        {
            Enderecos = new List<Endereco>();
        }

        public Cidade(string nome, string ddd)
        {
            Nome = nome;
            DDD = ddd;
        }

        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? DDD { get; set; }

        public virtual List<Endereco> Enderecos { get; set; }
    }
}
