using System;

namespace ApiCatalagoAnime.Entities
{
    public class Animes
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public string Genero { get; set; }
        public double Preco { get; set; }
    }
}
