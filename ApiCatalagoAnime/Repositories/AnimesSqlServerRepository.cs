using ApiCatalagoAnime.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiCatalagoAnime.Repositories
{
    public class AnimesSqlServerRepository : IAnimesRepository
    {

        private readonly SqlConnection sqlConnection;

        public AnimesSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Animes>> Obter(int pagina, int quantidade)
        {
            var animes = new List<Animes>();

            var comando = $@"select * from Animes order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                animes.Add(new Animes
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Genero = (string)sqlDataReader["Genero"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return animes;
        }


        public async Task<Animes> Obter(Guid id)
        {
            Animes animes = null;

            var comando = $@"select * from Animes where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                animes = new Animes
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Genero = (string)sqlDataReader["Genero"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return animes;
        }

        public async Task<List<Animes>> Obter(string nome, string produtora, string genero)
        {
            var animes = new List<Animes>();

            var comando = $@"select * from Animes where Nome = '{nome}' and Produtora = '{produtora}' and Genero = '{genero}' ";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                animes.Add(new Animes
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Genero = (string)sqlDataReader["Genero"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return animes;
        }

        public async Task Inserir(Animes animes)
        {
            var comando = $@"insert Animes (Id, Nome, Produtora, Genero, Preco) values ('{animes.Id}', '{animes.Nome}', '{animes.Produtora}', '{animes.Genero}',{animes.Preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Animes animes)
        {
            var comando = $@"update Animes set Nome = '{animes.Nome}', Produtora = '{animes.Produtora}', Genero = '{animes.Genero}', Preco = {animes.Preco.ToString().Replace(",", ".")} where Id = '{animes.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $@"delete from Jogos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }



    }
}
