using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services.Autor;
using WebAPI.Services.Livro;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*Diz que os metodos de IAutorInterface estão implementados em AutorServices, pois service sabe que precisa implementar
 os metodos da interface, porem a interface não sabe que o AutorService esta implementando os metodos*/
builder.Services.AddScoped<IAutorInterface, AutorService>();
builder.Services.AddScoped<ILivroInterface, LivroService>();

//configuração da conexão com o banco
builder.Services.AddDbContext<AppDbContext>(options => {
    //Ele entra no appsettings com o builder.configuration e com o getConnection ele acessa a ConnectionStrings
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
