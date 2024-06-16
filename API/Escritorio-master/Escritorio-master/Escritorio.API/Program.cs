using Escritorio.API.Endpoints;
using Escritorio.Shared.Dados.Banco;
using Escritorio.Shared.Modelos.Modelos;
using System.Text.Json.Serialization;

var seguranca = "Escritorio";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: seguranca,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5501")
                          .AllowAnyHeader()
                          .WithMethods("GET", "POST", "PUT", "DELETE");
                      });
});

builder.Services.AddDbContext<EscritorioContext>();
builder.Services.AddTransient<DAL<Cidade>>();
builder.Services.AddTransient<DAL<Cliente>>();
builder.Services.AddTransient<DAL<Endereco>>();
builder.Services.AddTransient<DAL<Login>>();
builder.Services.AddTransient<DAL<Propriedade>>();
builder.Services.AddTransient<DAL<Recibo>>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EscritorioContext>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.AddEndpointsCidade();
app.AddEndpointsCliente();
app.AddEndpointsEndereco();
app.AddEndpointsLogin();
app.AddEndpointsPropriedade();
app.AddEndpointsRecibo();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(seguranca);

app.Run();
