using ApiCatalagoAnime.Exceptions;
using ApiCatalagoAnime.Model.Input;
using ApiCatalagoAnime.Model.View;
using ApiCatalagoAnime.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoAnime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimesServices _animesServices;

        public AnimesController(IAnimesServices animesServices)
        {
            _animesServices = animesServices;
        }

        /// <summary>
        /// Buscar todos os animes de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os animes sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de animes</response>
        /// <response code="204">Caso não haja animes</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimesViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var animes = await _animesServices.Obter(pagina, quantidade);

            if (animes.Count() == 0)
                return NoContent();

            return Ok(animes);
        }

        /// <summary>
        /// Buscar um anime pelo seu Id
        /// </summary>
        /// <param name="idAnime">Id do anime buscado</param>
        /// <response code="200">Retorna o anime filtrado</response>
        /// <response code="204">Caso não haja anime com este id</response>   
        [HttpGet("{idAnime:guid}")]
        public async Task<ActionResult<AnimesViewModel>> Obter([FromRoute] Guid idAnime)
        {
            var anime = await _animesServices.Obter(idAnime);

            if (anime == null)
                return NoContent();

            return Ok(anime);
        }

        /// <summary>
        /// Inserir um anime no catálogo
        /// </summary>
        /// <param name="animesInput">Dados do anime a ser inserido</param>
        /// <response code="200">Cao o anime seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um anime com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<AnimesViewModel>> InserirAnime([FromBody] AnimesInputModel animesInput)
        {
            try
            {
                var anime = await _animesServices.Inserir(animesInput);

                return Ok(anime);
            }
            catch (AnimeJaCadastradoException)
            {
                return UnprocessableEntity("Já existe um anime com este nome para esta produtora!");
            }
        }

        /// <summary>
        /// Atualizar um anime no catálogo
        /// </summary>
        /// /// <param name="idAnime">Id do anime a ser atualizado</param>
        /// <param name="animesInput">Novos dados para atualizar o anime indicado</param>
        /// <response code="200">Caso o anime seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um anime com este Id</response>   
        [HttpPut("{idAnime:guid}")]
        public async Task<ActionResult> AtualizarAnime([FromRoute] Guid idAnime, [FromBody] AnimesInputModel animesInput)
        {
            try
            {
                await _animesServices.Atualizar(idAnime, animesInput);

                return Ok();
            }
            catch (AnimeNaoCadastradoException)
            {
                return NotFound("Não existe este anime!");
            }
        }

        /// <summary>
        /// Atualizar o preço de um anime
        /// </summary>
        /// /// <param name="idAnime">Id do anime a ser atualizado</param>
        /// <param name="preco">Novo preço do anime</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um anime com este Id</response>   
        [HttpPatch("{idAnime:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarAnime([FromRoute] Guid idAnime, [FromRoute] double preco)
        {
            try
            {
                await _animesServices.Atualizar(idAnime, preco);

                return Ok();
            }
            catch (AnimeNaoCadastradoException)
            {
                return NotFound("Não existe este anime!");
            }
        }

        /// <summary>
        /// Excluir um anime
        /// </summary>
        /// /// <param name="idAnime">Id do anime a ser excluído</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um anime com este Id</response>   
        [HttpDelete("{idAnime:guid}")]
        public async Task<ActionResult> ApagarAnime([FromRoute] Guid idAnime)
        {
            try
            {
                await _animesServices.Remover(idAnime);

                return Ok();
            }
            catch (AnimeNaoCadastradoException)
            {
                return NotFound("Não existe este anime!");
            }
        }


    }
}
