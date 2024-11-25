    using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO.Autor;
using WebAPI.Models;

namespace WebAPI.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        //classe apenas de leitura, para ter acesso ao banco de dados
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if(autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado";
                    return resposta;
                } 
                    resposta.Dados = autor;
                    resposta.Mensagem = "Autor Localizado!";
                    return resposta;
               

            } catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();

            try
            {
                //Entra na LivrosModel  da o include para entrar dentro de autor e pegar todos os dados do Autor
                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                if(livro == null)
                {
                    resposta.Mensagem = ("Autor não encontrado por este Id!");
                    return resposta;
                }

                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor Localizado!";
                return resposta;
            }
            catch (Exception ex)
            {
                //preenche a mensagem e status da classe ResponseModel
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            
            try
            {
                //entra no banco e transforma os autores em uma lista
                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Todos os Autores foram coletados!";

                return resposta;

            } catch (Exception ex)
            {
                //preenche a mensagem e status da classe ResponseModel
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            //ou pode simplificar ResponseModel<List<AutorModel>> resposta = new ();
            try
            {
                var autor = new AutorModel()
                {
                    Nome = autorCriacaoDto.Nome,
                    SobreNome = autorCriacaoDto.SobreNome
                };
                _context.Add(autor);
                //Espera que o nosso banco salve as alterações
                await _context.SaveChangesAsync();
                //Retorna uma lista de autores, já atualizada com o altor que criamos dentro de resposta.Dados
                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor Criado com Sucesso!";

                return resposta;
            } catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);
                
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    return resposta;
                }

                autor.Nome = autorEdicaoDto.Nome;
                autor.SobreNome = autorEdicaoDto.SobreNome;

                _context.Update(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor Editado com sucesso";
                return resposta;

            } catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    return resposta;
                }

                _context.Remove(autor);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor Removido com Sucesso!";

                return resposta;
            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
