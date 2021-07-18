using ApiCatalagoAnime.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoAnime.Repositories
{
    public class AnimesRepository : IAnimesRepository
    {
        private static Dictionary<Guid, Animes> animes = new Dictionary<Guid, Animes>()
        {


            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"),
                new Animes
                {
                    Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"),
                    Nome = "Shingeki no Kyojin",
                    Produtora = "WIT STUDIO",
                    Genero="Ação",
                    Preco = 200}
            },

            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"),
                new Animes{
                    Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"),
                    Nome = "Boku no hero academia",
                    Produtora = "BONES",
                    Genero="Ação",
                    Preco = 190}
            },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"),
                new Animes{
                    Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"),
                    Nome = "Vinland saga",
                    Produtora = "WIT STUDIO",
                    Genero="Drama",
                    Preco = 180}
            },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"),
                new Animes{
                    Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"),
                    Nome = "Hunter x Hunter",
                    Produtora = "MADHOUSE",
                    Genero="Ação",
                    Preco = 170}
            },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"),
                new Animes{
                    Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"),
                    Nome = "One Piece",
                    Produtora = "TOEI ANIMATION",
                    Genero="Ação",
                    Preco = 80}
            },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"),
                new Animes{
                    Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"),
                    Nome = "One Punch Man",
                    Produtora = "MADHOUSE",
                    Genero="Ação",
                    Preco = 190}
            }
        };


        public Task<List<Animes>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(animes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Animes> Obter(Guid id)
        {
            if (!animes.ContainsKey(id))
                return Task.FromResult<Animes>(null);

            return Task.FromResult(animes[id]);
        }

        public Task<List<Animes>> Obter(string nome, string produtora, string genero)
        {
            return Task.FromResult(animes.Values.Where(anime => anime.Nome.Equals(nome) && anime.Produtora.Equals(produtora) && anime.Genero.Equals(produtora)).ToList());
        }

        public Task<List<Animes>> ObterSemLambda(string nome, string produtora, string genero)
        {
            var retorno = new List<Animes>();

            foreach (var anime in animes.Values)
            {
                if (anime.Nome.Equals(nome) && anime.Produtora.Equals(produtora) && anime.Genero.Equals(produtora))
                    retorno.Add(anime);
            }

            return Task.FromResult(retorno);
        }


        public Task Inserir(Animes anime)
        {
            animes.Add(anime.Id, anime);
            return Task.CompletedTask;
        }


        public Task Atualizar(Animes anime)
        {
            animes[anime.Id] = anime;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            animes.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
