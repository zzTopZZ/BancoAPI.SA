using BancoAPI.SA.Data;
using BancoAPI.SA.Services.Cliente;
using BancoAPI.SA.Services.ContaCorrente;
using BancoAPI.SA.Services.Transacao;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteInterface, ClienteService>();
builder.Services.AddScoped<ITransacaoInterface, TransacaoService>();
builder.Services.AddScoped<IContaCorrenteInterface, ContaCorrenteService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaltConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
