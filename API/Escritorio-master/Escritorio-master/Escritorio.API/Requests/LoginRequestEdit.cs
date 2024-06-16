namespace Escritorio.API.Requests
{
    public record LoginRequestEdit(int Id, string Usuario, string Senha)
    : LoginRequest(Usuario, Senha);
}
