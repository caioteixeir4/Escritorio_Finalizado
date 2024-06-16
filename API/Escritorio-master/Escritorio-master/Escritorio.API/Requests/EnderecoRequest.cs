namespace Escritorio.API.Requests
{
    public record EnderecoRequest(string Rua, string Bairro, string CEP, int CidadeId);
}
