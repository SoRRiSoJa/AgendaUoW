using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaUoW.Services
{
    using AgendaUoW.Domain.Models;
    using AgendaUoW.Domain.Repositories;
    using AgendaUoW.Domain.Services;

    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        public ContatoService(IContatoRepository _contatoRepository)
        {
            this._contatoRepository = _contatoRepository ?? throw new ArgumentNullException(nameof(_contatoRepository));
        }
        public Task<Contato> Editar(int idContato, Contato contato)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(decimal idContato)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contato>> Listar()
        {
            return _contatoRepository.Listar();
        }

        public Task<IEnumerable<Contato>> ObterPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contato>> ObterPorNumero(string numero)
        {
            throw new NotImplementedException();
        }

        public Task<Contato> Salvar(Contato contato)
        {
            throw new NotImplementedException();
        }
    }
}
