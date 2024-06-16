using Escritorio.API.Requests;
using Escritorio.Shared.Dados.Banco;
using Escritorio.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Escritorio.API.Endpoints
{
        public static class ClienteExtensions
        {
            public static void AddEndpointsCliente(this WebApplication app)
            {

                #region Endpoint Cliente
                //Exibir todos os cliente
                app.MapGet("/Cliente", ([FromServices] DAL<Cliente> dal) =>
                {
                    return Results.Ok(dal.Listar());
                });

                //Exibir um cliente
                app.MapGet("/Cliente/{nome}", ([FromServices] DAL<Cliente> dal, string nome) =>
                {
                    var cliente = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                    if (cliente is null)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(cliente);

                });

                //Adiconar
                app.MapPost("/Cliente", ([FromServices] DAL<Cliente> dal, [FromBody] ClienteRequest clienteRequest) =>
                {
                    //serve para a api só aceitar para cadastrar o nome e demais itens
                    var cliente = new Cliente(clienteRequest.Nome,
                        clienteRequest.CPF,clienteRequest.RG,clienteRequest.Celular, clienteRequest.EnderecoId);

                    dal.Adicionar(cliente);
                    return Results.Ok(cliente);
                });

                //Deletar
                app.MapDelete("/Cliente/{id}", ([FromServices] DAL<Cidade> dal, int id) =>
                {
                    var cliente = dal.RecuperarPor(a => a.Id == id);
                    if (cliente is null)
                    {
                        return Results.NotFound();
                    }
                    dal.Deletar(cliente);
                    return Results.NoContent();

                });

                //Alterar
                app.MapPut("/Cliente", ([FromServices] DAL<Cliente> dal, [FromBody] ClienteRequestEdit clienteRequestEdit) =>
                {
                    var clienteAtualizar = dal.RecuperarPor(a => a.Id == clienteRequestEdit.Id);
                    if (clienteAtualizar is null)
                    {
                        return Results.NotFound();
                    }
                    clienteAtualizar.Nome = clienteRequestEdit.Nome;
                    clienteAtualizar.CPF = clienteRequestEdit.CPF;
                    clienteAtualizar.RG = clienteRequestEdit.RG;
                    clienteAtualizar.Celular = clienteRequestEdit.Celular;

                   

                    dal.Atualizar(clienteAtualizar);
                    return Results.Ok();
                });
                #endregion
            }
        }
}
