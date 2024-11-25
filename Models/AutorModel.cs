using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        /* JsonIgnore faz com que não apareça a lista de livros, ele ignora a lista no json pois essa propriedade só serve 
         * para fazer a correlação entre autor e livro */
        [JsonIgnore]
        /* ICollection faz com que o Autor tenha uma coleção de livros, ou seja uma lista de livros */
        public ICollection<LivroModel> Livros { get; set; }
    }
}
