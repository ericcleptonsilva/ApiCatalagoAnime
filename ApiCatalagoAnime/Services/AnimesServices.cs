using ApiCatalagoAnime.Entities;
using ApiCatalagoAnime.Exceptions;
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


        public async Task<AnimesViewModel> Obter(Guid id)
        {
            var animes = await _animesRepository.Obter(id);

            if (animes == null)
                return null;
            return new AnimesViewModel
            {
                Id = animes.Id,
                Nome = animes.Nome,
                Produtora = animes.Produtora,
                Genero = animes.Genero,
                Preco = animes.Preco
            };
        }


        public async Task<AnimesViewModel> Inserir(AnimesInputModel anime)
        {
            var entidadeAnime = await _animesRepository.Obter(anime.Nome, anime.Produtora, anime.Genero);

            if (entidadeAnime.Count > 0)
                throw new AnimeJaCadastradoException();
            var AnimeInsert = new Animes
            { 
                Id = Guid.NewGuid(),
                Nome = anime.Nome,
                Produtora = anime.Produtora,
                Genero = anime.Genero,
                Preco = anime.Preco
            };

            await _animesRepository.Inserir(AnimeInsert);

            return new AnimesViewModel
            { 
                Id = AnimeInsert.Id,
                Nome = anime.Nome,
                Produtora = anime.Produtora,
                Genero = anime.Genero,
                Preco = anime.Preco
            };
        }


        public async Task Atualizar(Guid id, AnimesInputModel anime)
        {
            var entidadeAnime = await _animesRepository.Obter(id);

            if (entidadeAnime == null)
                throw new AnimeNaoCadastradoException();

            entidadeAnime.Nome = anime.Nome;
            entidadeAnime.Produtora = anime.Produtora;
            entidadeAnime.Genero = anime.Genero;
            entidadeAnime.Preco = anime.Preco;

            await _animesRepository.Atualizar(entidadeAnime);

        }


        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeAnime = await _animesRepository.Obter(id);

            if (entidadeAnime == null)
                throw new AnimeNaoCadastradoException();
            entidadeAnime.Preco = preco;

            await _animesRepository.Atualizar(entidadeAnime);

        }


        public async Task Remover(Guid id)
        {
            var anime = await _animesRepository.Obter(id);
            if (anime == null)
                throw new AnimeNaoCadastradoException();
            await _animesRepository.Remover(id);
        }

        public void Dispose()
        {
            _animesRepository?.Dispose();
        }
    }
}
