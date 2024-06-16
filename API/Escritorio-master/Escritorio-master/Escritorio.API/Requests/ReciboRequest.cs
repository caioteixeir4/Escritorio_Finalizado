namespace Escritorio.API.Requests
{
    public record ReciboRequest(string Referencia, decimal Valor, string Status, string Comprovante, int PropriedadeId);
}
