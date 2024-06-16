using Escritorio.API.Requests;
using Escritorio.Shared.Dados.Banco;
using Escritorio.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Escritorio.API.Endpoints
{
    public static class PropriedadeExtensions
    {
        public static void AddEndpointsPropriedade(this WebApplication app)
        {

            #region Endpoint Propriedade
            //Exibir todos as propriedades
            app.MapGet("/Propriedade", ([FromServices] DAL<Propriedade> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            //Exibir uma propriedade
            app.MapGet("/Propriedade/{nome}", ([FromServices] DAL<Propriedade> dal, string nome) =>
            {
                var propriedade = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (propriedade is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(propriedade);

            });

            //Adiconar
            app.MapPost("/Propriedade", ([FromServices] DAL<Propriedade> dal, [FromBody] PropriedadeRequest propriedadeRequest) =>
            {
                //serve para a api só aceitar para cadastrar o nome e a bio do endereco
                var propriedade = new Propriedade(propriedadeRequest.Nome,
                    propriedadeRequest.CNPJ, propriedadeRequest.InscricaoEstadual, propriedadeRequest.Status,
                    propriedadeRequest.NumPasta,
                    propriedadeRequest.EnderecoId, propriedadeRequest.ClienteId);

                dal.Adicionar(propriedade);
                return Results.Ok(propriedade);
            });

            //Deletar
            app.MapDelete("/Propriedade/{id}", ([FromServices] DAL<Propriedade> dal, int id) => {
                var propriedade = dal.RecuperarPor(a => a.Id == id);
                if (propriedade is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(propriedade);
                return Results.NoContent();

            });

            //Alterar
            app.MapPut("/Propriedade", ([FromServices] DAL<Propriedade> dal, [FromBody] PropriedadeRequestEdit propriedadeRequestEdit) => {
                var propriedadeAtualizar = dal.RecuperarPor(a => a.Id == propriedadeRequestEdit.Id);
                if (propriedadeAtualizar is null)
                {
                    return Results.NotFound();
                }
                propriedadeAtualizar.Nome = propriedadeRequestEdit.Nome;
                propriedadeAtualizar.CNPJ = propriedadeRequestEdit.CNPJ;
                propriedadeAtualizar.InscricaoEstadual = propriedadeRequestEdit.InscricaoEstadual;
                propriedadeAtualizar.Status = propriedadeRequestEdit.Status;
                propriedadeAtualizar.NumPasta = propriedadeRequestEdit.NumPasta;
                propriedadeAtualizar.EnderecoId = propriedadeRequestEdit.EnderecoId;
                propriedadeAtualizar.ClienteId = propriedadeRequestEdit.ClienteId;

                dal.Atualizar(propriedadeAtualizar);
                return Results.Ok();
            });
            #endregion
        }
    }
}
