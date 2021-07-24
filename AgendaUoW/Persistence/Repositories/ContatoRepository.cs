using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaUoW.Persistence.Repositories
{
    using AgendaUoW.Domain.Models;
    using AgendaUoW.Domain.Repositories;
    using AgendaUoW.Middlewares;
    using AgendaUoW.Persistence.Config;


    public class ContatoRepository : IContatoRepository
    {
        private DbSession _session;
        public ContatoRepository(DbSession _session)
        {
            this._session = _session ?? throw new ArgumentNullException(nameof(_session));
        }

        public async Task<Contato> Editar(decimal Id, Contato contato)
        {
            try
            {
                var query = "UPDATE contato SET nome=@Nome, numero=@Numero, ref=@Ref, isAtivo=@IsAtivo WHERE codigo=@Id)";
                await _session.Connection.ExecuteAsync(query, new { contato.Nome, contato.Numero, contato.Ref, contato.IsAtivo, Id }, _session.Transaction);
                _session.Dispose();
                return contato;

            }
            catch (Exception)
            {
                _session.Transaction.Rollback();
                throw new HttpResponseException(500, $"Ocorreu um erro ao atualizar o registro.");
            }
        }

        public async Task<bool> Excluir(decimal idcontato)
        {
            try
            {
                var query = "UPDATE contato SET isAtivo=0 WHERE codigo=@Id)";
                await _session.Connection.ExecuteAsync(query, new { Id = idcontato }, _session.Transaction);
                _session.Dispose();
                return true;

            }
            catch (Exception)
            {
                _session.Transaction.Rollback();
                throw new HttpResponseException(500, $"Ocorreu um erro ao excluir o registro.");
            }

        }

        public async Task<IEnumerable<Contato>> Listar()
        {
            try
            {
                var query = "SELECT * FROM contato WHERE isAtivo = 1";
                var result = await _session.Connection.QueryAsync<Contato>(query, null, _session.Transaction);
                _session.Dispose();
                return result;
            }
            catch (Exception)
            {
                throw new HttpResponseException(401, $"nenhum registro encontrado");
            }

        }

        public async Task<Contato> Obter(decimal idcontato)
        {
            try
            {
                var query = "SELECT * FROM contato WHERE codigo = @idcontato AND isAtivo = 1";
                var result = await _session.Connection.QueryFirstOrDefaultAsync<Contato>(query, new { idcontato }, _session.Transaction);
                _session.Dispose();
                return result;
            }
            catch (Exception)
            {

                throw new HttpResponseException(401, $"Erro ao realizar consulta");
            }

        }

        public async Task<IEnumerable<Contato>> ObterPorNome(string nome)
        {
            try
            {

                var query = $"SELECT * FROM contato WHERE nome like  @nome AND isAtivo = 1";
                var result = await _session.Connection.QueryAsync<Contato>(query, new { nome = $"%{nome}%" }, _session.Transaction);
                _session.Dispose();
                return result;
            }
            catch (Exception)
            {
                throw new HttpResponseException(401, $"Erro ao realizar consulta");
            }

        }

        public async Task<IEnumerable<Contato>> ObterPorNumero(string numero)
        {
            try
            {
                var query = "SELECT * FROM contato WHERE nome like @numero AND isAtivo = 1";
                var result = await _session.Connection.QueryAsync<Contato>(query, new { numero = $"%{numero}%" }, _session.Transaction);
                _session.Dispose();
                return result;
            }
            catch (Exception)
            {
                throw new HttpResponseException(401, $"Erro ao realizar consulta");
            }

        }

        public async Task<Contato> Salvar(Contato contato)
        {
            try
            {
                var query = "INSERT INTO contato (nome,numero,ref,isAtivo) OUTPUT INSERTED.codigo VALUES (@Nome,@Numero,@Ref,@IsAtivo)";
                var idContato = await _session.Connection.ExecuteAsync(query, new { contato.Nome, contato.Numero, contato.Ref, contato.IsAtivo }, _session.Transaction);
                contato.Id = idContato;
                _session.Dispose();
                return contato;
            }
            catch (Exception)
            {
                throw new HttpResponseException(500, $"Ocorreu um erro ao salvar o registro.");
            }

        }
    }
}
