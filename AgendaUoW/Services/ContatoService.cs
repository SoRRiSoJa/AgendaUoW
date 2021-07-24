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
        public async Task<Contato> Editar(int idContato, Contato contato)
        {
            return await _contatoRepository.Editar(idContato, contato);
        }

        public async Task<bool> Excluir(decimal idContato)
        {
            return await _contatoRepository.Excluir(idContato);
        }

        public async Task<IEnumerable<Contato>> Listar()
        {
            return await _contatoRepository.Listar();
        }

        public async Task<IEnumerable<Contato>> ObterPorNome(string nome)
        {
            return await _contatoRepository.ObterPorNome(nome);
        }

        public async Task<IEnumerable<Contato>> ObterPorNumero(string numero)
        {
            return await _contatoRepository.ObterPorNumero(numero);
        }

        public async Task<Contato> Salvar(Contato contato)
        {
            return await _contatoRepository.Salvar(contato); ;
        }
    }
}
