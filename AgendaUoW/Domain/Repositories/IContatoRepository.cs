using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaUoW.Domain.Repositories
{
    using AgendaUoW.Domain.Models;
    public interface IContatoRepository
    {
        Task<Contato> Salvar(Contato contato);
        Task<Contato> Editar(decimal Id, Contato contato);
        Task<bool> Excluir(decimal idcontato);
        Task<IEnumerable<Contato>> Listar();
        Task<Contato> Obter(decimal idcontato);
        Task<IEnumerable<Contato>> ObterPorNome(string nome);
        Task<IEnumerable<Contato>> ObterPorNumero(string numero);
    }
}
