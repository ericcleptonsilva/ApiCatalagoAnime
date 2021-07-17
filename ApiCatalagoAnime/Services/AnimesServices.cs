using ApiCatalagoAnime.Model.Input;
using ApiCatalagoAnime.Model.View;
using ApiCatalagoAnime.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoAnime.Services
{
    public class AnimesServices : IAnimesServices
    {
        private readonly IAnimesRepository _animesRepository;

        public AnimesServices(IAnimesRepository animesRepository)
        {
            _animesRepository = animesRepository;

        }


        public async Task<List<AnimesViewModel>> Obter(int pagina, int quantidade)
        {
            var animes = await _animesRepository.Obter(pagina, quantidade);

            return animes.Select(anime => new AnimesViewModel
            {
                Id = anime.Id,
                Nome = anime.Nome,
                Produtora = anime.Produtora,
                Genero = anime.Genero,
                Preco = anime.Preco

            }).ToList();
        }


        public Task<AnimesViewModel> Obter(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<AnimesViewModel> Inserir(AnimesInputModel anime)
        {
            throw new NotImplementedException();
        }


        public Task Atualizar(Guid id, AnimesInputModel anime)
        {
            throw new NotImplementedException();
        }


        public Task Atualizar(Guid id, double preco)
        {
            throw new NotImplementedException();
        }


        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
