using Autoflex.Application.Interfaces;
using Autoflex.Application.Services;
using Autoflex.Domain.Interfaces;
using Autoflex.Infrastructure.Data;
using Autoflex.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar o Banco de Dados (já fizemos, mas garanta que esteja aqui)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar os Repositórios (Infra <-> Domain)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRawMaterialRepository, RawMaterialRepository>();
builder.Services.AddScoped<IProductIngredientRepository, ProductIngredientRepository>();

// 3. Registrar os Application Services (Application <-> API)
builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<IProductionService, ProductionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar o Swagger para testarmos a API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();