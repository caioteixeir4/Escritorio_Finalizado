namespace Escritorio.API.Requests
{
    public record ReciboRequestEdit(int Id, string Referencia, decimal Valor,
        string Status, string Comprovante, int PropriedadeId)
        : ReciboRequest(Referencia, Valor, Status, Comprovante, PropriedadeId);
}
