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
        public async Task<Contato> Editar(decimal idContato, Contato contato)
        {
            var novoContato = await _contatoRepository.Obter(idContato);
            if (idContato == 0)
            {
                throw new HttpResponseException(404, $"Você está me tirando? Forneça um id válido.");
            }

            if (novoContato == null)
            {
                throw new HttpResponseException(401, $"Registro não encontarado");
            }
            else 
            {
                novoContato.Nome = contato.Nome;
                novoContato.Numero = contato.Numero;
                try
                {
                    _unitOfWork.BeginTransaction();
                    novoContato = await _contatoRepository.Editar(idContato, novoContato);
                    _unitOfWork.Commit();
                    return novoContato;
                }
                catch (Exception)
                {

                    _unitOfWork.Rollback();
                    throw new HttpResponseException(500, $"Ocorreu um erro ao editar o registro.");
                }
            }
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
            if (string.IsNullOrEmpty(nome))
            {
                throw new HttpResponseException(404, $"Você deve fornecer um nome válido.");
            }
            return await _contatoRepository.ObterPorNome(nome);
        }

        public async Task<IEnumerable<Contato>> ObterPorNumero(string numero)
        {
            if (string.IsNullOrEmpty(numero))
            {
                throw new HttpResponseException(404, $"Você deve fornecer um número válido.");
            }

            return await _contatoRepository.ObterPorNumero(numero);
        }

        public async Task<Contato> Salvar(Contato contato)
        {
            if (contato==null)
            {
                throw new HttpResponseException(404, $"Preencha os dados corretamente.");
            }

            try
            {
                _unitOfWork.BeginTransaction();
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
