using Escritorio.API.Requests;
using Escritorio.Shared.Dados.Banco;
using Escritorio.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Escritorio.API.Endpoints
{
    public static class CidadeExtensions
    {
        public static void AddEndpointsCidade(this WebApplication app)
        {

            #region Endpoint Cidade
            //Exibir todos as cidades
            app.MapGet("/Cidade", ([FromServices] DAL<Cidade> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            //Exibir uma cidade
            app.MapGet("/Cidade/{nome}", ([FromServices] DAL<Cidade> dal, string nome) =>
            {
                var cidade = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (cidade is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(cidade);

            });

            //Adiconar
            app.MapPost("/Cidade", ([FromServices] DAL<Cidade> dal, [FromBody] CidadeRequest cidadeRequest) =>
            {
                //serve para a api só aceitar para cadastrar o nome e a bio do artista
                var cidade = new Cidade(cidadeRequest.Nome,
                    cidadeRequest.DDD);

                dal.Adicionar(cidade);
                return Results.Ok(cidade);
            });

            //Deletar
            app.MapDelete("/Cidade/{id}", ([FromServices] DAL<Cidade> dal, int id) =>
            {
                var cidade = dal.RecuperarPor(a => a.Id == id);
                if (cidade is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(cidade);
                return Results.NoContent();

            });

            //Alterar
            app.MapPut("/Cidade", ([FromServices] DAL<Cidade> dal, [FromBody] CidadeRequestEdit cidadeRequestEdit) =>
            {
                var cidadeAtualizar = dal.RecuperarPor(a => a.Id == cidadeRequestEdit.Id);
                if (cidadeAtualizar is null)
                {
                    return Results.NotFound();
                }
                cidadeAtualizar.Nome = cidadeRequestEdit.Nome;
                cidadeAtualizar.DDD = cidadeRequestEdit.DDD;

                dal.Atualizar(cidadeAtualizar);
                return Results.Ok();
            });
            #endregion
        }
    }
}

