namespace WebAPI.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        //Abaixo quer dizer que o LivroModel tera um Autor associado a ele
        public AutorModel Autor { get; set; }
    }
}
