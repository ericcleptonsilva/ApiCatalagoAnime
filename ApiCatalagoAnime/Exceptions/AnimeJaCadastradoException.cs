using System;


namespace ApiCatalagoAnime.Exceptions
{
    public class AnimeJaCadastradoException : Exception
    {
        public AnimeJaCadastradoException() : base("Este Anime já esta cadastrado!"){ }
    }
}
