namespace Escritorio.API.Requests
{
    public record PropriedadeRequest(string Nome, string CNPJ, string InscricaoEstadual,
        string Status, int NumPasta, int EnderecoId,
        int ClienteId);
}
