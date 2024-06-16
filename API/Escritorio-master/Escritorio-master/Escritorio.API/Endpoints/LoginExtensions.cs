using Escritorio.API.Requests;
using Escritorio.Shared.Dados.Banco;
using Escritorio.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Escritorio.API.Endpoints
{
    public static class LoginExtensions
    {
        public static void AddEndpointsLogin(this WebApplication app)
        {

            #region Endpoint Login
            //Exibir todos os logins
            app.MapGet("/Login", ([FromServices] DAL<Login> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            //Exibir um login
            app.MapGet("/Login/{usuario}", ([FromServices] DAL<Login> dal, string usuario) =>
            {
                var login = dal.RecuperarPor(a => a.Usuario.ToUpper().Equals(usuario.ToUpper()));
                if (login is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(usuario);

            });

            //Adiconar
            app.MapPost("/Login", ([FromServices] DAL<Login> dal, [FromBody] LoginRequest loginRequest) =>
            {
                //serve para a api só aceitar para cadastrar o nome e a bio do artista
                var login = new Login(loginRequest.Usuario,
                    loginRequest.Senha);

                dal.Adicionar(login);
                return Results.Ok(login);
            });

            //Deletar
            app.MapDelete("/Login/{id}", ([FromServices] DAL<Login> dal, int id) => {
                var login = dal.RecuperarPor(a => a.Id == id);
                if (login is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(login);
                return Results.NoContent();

            });

            //Alterar
            app.MapPut("/Login", ([FromServices] DAL<Login> dal, [FromBody] LoginRequestEdit loginRequestEdit) => {
                var loginAtualizar = dal.RecuperarPor(a => a.Id == loginRequestEdit.Id);
                if (loginAtualizar is null)
                {
                    return Results.NotFound();
                }
                loginAtualizar.Usuario = loginRequestEdit.Usuario;
                loginAtualizar.Senha = loginRequestEdit.Senha;

                dal.Atualizar(loginAtualizar);
                return Results.Ok();
            });
            #endregion
        }
    }
}
