namespace Escritorio.API.Requests
{
    public record EnderecoRequestEdit(int Id, string Rua, string Bairro, string CEP, int CidadeId)
    : EnderecoRequest(Rua, Bairro, CEP, CidadeId);
    
}
