using AutoMapper;
namespace AgendaUoW.Resources.Profiles
{
    using AgendaUoW.Domain.Models;
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<Contato, ContatoResource>();
        }
    }
}
