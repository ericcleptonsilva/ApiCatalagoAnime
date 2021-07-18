using ApiCatalagoAnime.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalagoAnime.Repositories
{
    public interface IAnimesRepository : IDisposable
    {
        Task<List<Animes>> Obter(int pagina, int quantidade);
        Task<Animes> Obter(Guid id);
        Task<List<Animes>> Obter(string nome, string produtora, string genero);
        Task Inserir(Animes animes);
        Task Atualizar(Animes animes);
        Task Remover(Guid id);

    }
}
