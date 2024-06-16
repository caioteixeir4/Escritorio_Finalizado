namespace Escritorio.API.Requests
{
    public record ClienteRequestEdit(int Id, string Nome, string CPF, string RG, string Celular, int EnderecoId)
    : ClienteRequest(Nome, CPF, RG, Celular, EnderecoId);
}
