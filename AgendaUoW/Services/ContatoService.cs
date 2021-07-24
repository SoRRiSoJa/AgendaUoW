using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaUoW.Services
{
    using AgendaUoW.Domain.Models;
    using AgendaUoW.Domain.Repositories;
    using AgendaUoW.Domain.Services;
    using AgendaUoW.Domain.UoW;
    using AgendaUoW.Middlewares;

    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ContatoService(IUnitOfWork _unitOfWork, IContatoRepository _contatoRepository)
        {

            this._unitOfWork = _unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
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
            try
            {
                _unitOfWork.Begintransaction();
                var result= await _contatoRepository.Salvar(contato); ;
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw new HttpResponseException(500, $"Ocorreu um erro ao salvar o registro.");
            }
            
        }
    }
}
