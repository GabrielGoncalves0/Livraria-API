using WebAPI.DTO.Vinculo;

namespace WebAPI.DTO.Livro
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public autorVinculoDto Autor { get; set; }
    }
}
