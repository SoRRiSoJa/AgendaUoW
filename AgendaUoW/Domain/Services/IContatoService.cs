using AgendaUoW.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaUoW.Domain.Services
{
    public interface IContatoService
    {
        Task<Contato> Salvar(Contato contato);
        Task<Contato> Editar(int idContato, Contato contato);
        Task<bool> Excluir(decimal idContato);
        Task<IEnumerable<Contato>> ObterPorNome(string nome);
        Task<IEnumerable<Contato>> ObterPorNumero(string numero);
        Task<IEnumerable<Contato>> Listar();
    }
}
