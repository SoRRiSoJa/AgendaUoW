using Dapper.FluentMap.Dommel.Mapping;

namespace AgendaUoW.Persistence.Mapper
{
    using AgendaUoW.Domain.Models;
    public class ContatoMap : DommelEntityMap<Contato>
    {
        public ContatoMap()
        {
            ToTable("contato");
            Map((x) => x.Id).ToColumn("codigo").IsKey();
            Map((x) => x.Nome).ToColumn("nome").IsKey();
            Map((x) => x.Numero).ToColumn("numero").IsKey();
            Map((x) => x.Ref).ToColumn("ref").IsKey();
            Map((x) => x.IsAtivo).ToColumn("isAtivo").IsKey();
        }
    }
}
