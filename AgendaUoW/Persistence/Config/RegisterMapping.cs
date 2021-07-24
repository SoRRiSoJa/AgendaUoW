using Dapper.FluentMap;

namespace AgendaUoW.Persistence.Config
{
    using AgendaUoW.Persistence.Mapper;
    public static class RegisterMapping
    {
        public static void Register()
        {
            FluentMapper.Initialize((config) =>
            {
                config.AddMap(new ContatoMap());
            });
        }
    }
}
