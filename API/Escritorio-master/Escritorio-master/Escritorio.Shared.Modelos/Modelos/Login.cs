using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Modelos.Modelos
{
    public class Login
    {
        public Login() { }
        public Login(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }
        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
