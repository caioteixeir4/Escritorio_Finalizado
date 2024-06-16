using Escritorio.API.Requests;
using Escritorio.Shared.Dados.Banco;
using Escritorio.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Escritorio.API.Endpoints
{
    public static class EnderecoExtensions
    {
        public static void AddEndpointsEndereco(this WebApplication app)
        {

            #region Endpoint Endereco
            //Exibir todos os Enderecos
            app.MapGet("/Endereco", ([FromServices] DAL<Endereco> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            //Exibir um Endereco
            app.MapGet("/Endereco/{rua}", ([FromServices] DAL<Endereco> dal, string rua) =>
            {
                var endereco = dal.RecuperarPor(a => a.Rua.ToUpper().Equals(rua.ToUpper()));
                if (endereco is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(endereco);

            });

            //Adiconar
            app.MapPost("/Endereco", ([FromServices] DAL<Endereco> dal, [FromBody] EnderecoRequest enderecoRequest) =>
            {
                //serve para a api só aceitar para cadastrar o nome e a bio do endereco
                var endereco = new Endereco(enderecoRequest.Rua,
                    enderecoRequest.Bairro, enderecoRequest.CEP, enderecoRequest.CidadeId);

                dal.Adicionar(endereco);
                return Results.Ok(endereco);
            });

            //Deletar
            app.MapDelete("/Endereco/{id}", ([FromServices] DAL<Endereco> dal, int id) => {
                var endereco = dal.RecuperarPor(a => a.Id == id);
                if (endereco is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(endereco);
                return Results.NoContent();

            });

            //Alterar
            app.MapPut("/Endereco", ([FromServices] DAL<Endereco> dal, [FromBody] EnderecoRequestEdit enderecoRequestEdit) => {
                var enderecoAtualizar = dal.RecuperarPor(a => a.Id == enderecoRequestEdit.Id);
                if (enderecoAtualizar is null)
                {
                    return Results.NotFound();
                }
                enderecoAtualizar.Rua = enderecoRequestEdit.Rua;
                enderecoAtualizar.Bairro = enderecoRequestEdit.Bairro;
                enderecoAtualizar.CEP = enderecoRequestEdit.CEP;
                enderecoAtualizar.CidadeId = enderecoRequestEdit.CidadeId;

                dal.Atualizar(enderecoAtualizar);
                return Results.Ok();
            });
            #endregion
        }
    }
}
