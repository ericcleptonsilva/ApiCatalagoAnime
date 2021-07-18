using ApiCatalagoAnime.Model.Input;
using ApiCatalagoAnime.Model.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalagoAnime.Services
{
    public interface IAnimesServices : IDisposable
    {

        Task<List<AnimesViewModel>> Obter(int pagina, int quantidade);
        Task<AnimesViewModel> Obter(Guid id);
        Task<AnimesViewModel> Inserir(AnimesInputModel anime);
        Task Atualizar(Guid id, AnimesInputModel anime);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
