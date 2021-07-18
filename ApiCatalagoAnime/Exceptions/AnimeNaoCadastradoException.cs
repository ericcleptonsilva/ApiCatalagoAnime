using System;



namespace ApiCatalagoAnime.Exceptions
{
    public class AnimeNaoCadastradoException : Exception
    {
        public AnimeNaoCadastradoException() : base("Este anime não esta cadastrdo!") { }
    }
}
