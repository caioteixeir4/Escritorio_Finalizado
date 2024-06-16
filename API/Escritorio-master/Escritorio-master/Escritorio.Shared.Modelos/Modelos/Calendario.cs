using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escritorio.Shared.Modelos.Modelos
{
    public class Calendario
    {
        public Calendario()
        {

        }

        public Calendario(int dia, int mes, int ano, string texto)
        {
            Dia = dia;
            Mes = mes;
            Ano = ano;
            Texto = texto;
        }

        [Key]
        public int Id { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string? Texto { get; set; }

    }
}
