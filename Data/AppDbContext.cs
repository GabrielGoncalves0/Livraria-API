using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    //Se comunica com o banco de dados atraves desse contexto que estamos criando usando alguns pacotes como

    public class AppDbContext : DbContext
    {
        //A classe AppDbContext herda de DbContext e dentro do constructor dela passamos as opções do banco como qual banco, nome, conexão e etc...
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }
    }
}

    /*para criar a migration digitamos em ferramentas, gerenciador de pacotes do nuget, console do gerenciador de pacotes e
    digitamos add-migration CriandoBancoDeDados*/
    /*Depois basta dar um update-database para realmente fazer as modificações no banco de dados*/