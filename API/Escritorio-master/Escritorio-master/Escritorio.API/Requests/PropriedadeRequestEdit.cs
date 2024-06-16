namespace Escritorio.API.Requests
{
    public record PropriedadeRequestEdit(int Id, string Nome, string CNPJ, string InscricaoEstadual,
        string Status, int NumPasta, int EnderecoId,
        int ClienteId)
        : PropriedadeRequest(Nome, CNPJ, InscricaoEstadual,
        Status, NumPasta, EnderecoId,
        ClienteId);
}
