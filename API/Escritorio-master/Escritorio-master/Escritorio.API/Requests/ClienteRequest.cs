using System.ComponentModel.DataAnnotations;

namespace Escritorio.API.Requests
{
    public record ClienteRequest(string Nome, string CPF, string RG, string Celular, int EnderecoId);
}
