using ApiBCB.AD.Models;
using ApiBCB.AD.Services.Productos;
using ApiBCB.AD.Services.Categorias;
using ApiBCB.AD.Services.Envases;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var connection = new M_ConnectionToSql(config.GetConnectionString("SQL"));

// Services
builder.Services.AddSingleton(connection);
builder.Services.AddTransient<S_IProductos, S_Productos>();
builder.Services.AddTransient<S_ICategorias, S_Categorias>();
builder.Services.AddTransient<S_IEnvases, S_Envases>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
