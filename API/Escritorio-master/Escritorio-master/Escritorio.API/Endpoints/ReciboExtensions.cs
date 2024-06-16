using Escritorio.API.Requests;
using Escritorio.Shared.Dados.Banco;
using Escritorio.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Escritorio.API.Endpoints
{
    public static class ReciboExtensions
    {
        public static void AddEndpointsRecibo(this WebApplication app)
        {

            #region Endpoint Recibo
            //Exibir todos os recibos
            app.MapGet("/Recibo", ([FromServices] DAL<Recibo> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            //Exibir um recibo
            app.MapGet("/Recibo/{referencia}", ([FromServices] DAL<Recibo> dal, string referencia) =>
            {
                var recibo = dal.RecuperarPor(a => a.Referencia.ToUpper().Equals(referencia.ToUpper()));
                if (recibo is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(recibo);

            });

            //Adiconar
            app.MapPost("/Recibo", ([FromServices] DAL<Recibo> dal, [FromBody] ReciboRequest reciboRequest) =>
            {
                //serve para a api só aceitar para cadastrar o nome e a bio do artista
                var recibo = new Recibo(reciboRequest.Referencia, reciboRequest.Valor,
                    reciboRequest.Status, reciboRequest.Comprovante, reciboRequest.PropriedadeId);

                dal.Adicionar(recibo);
                return Results.Ok(recibo);
            });

            //Deletar
            app.MapDelete("/Recibo/{id}", ([FromServices] DAL<Recibo> dal, int id) => {
                var recibo = dal.RecuperarPor(a => a.Id == id);
                if (recibo is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(recibo);
                return Results.NoContent();

            });

            //Alterar
            app.MapPut("/Recibo", ([FromServices] DAL<Recibo> dal, [FromBody] ReciboRequestEdit reciboRequestEdit) => {
                var reciboAtualizar = dal.RecuperarPor(a => a.Id == reciboRequestEdit.Id);
                if (reciboAtualizar is null)
                {
                    return Results.NotFound();
                }
                reciboAtualizar.Referencia = reciboRequestEdit.Referencia;
                reciboAtualizar.Valor = reciboRequestEdit.Valor;
                reciboAtualizar.Status = reciboRequestEdit.Status;
                reciboAtualizar.Comprovante = reciboRequestEdit.Comprovante;
                reciboAtualizar.PropriedadeId = reciboRequestEdit.PropriedadeId;

                dal.Atualizar(reciboAtualizar);
                return Results.Ok();
            });
            #endregion
        }
    }
}
