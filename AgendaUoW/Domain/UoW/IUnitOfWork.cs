using System;

namespace AgendaUoW.Domain.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        void Begintransaction();
        void Commit();
        void Rollback();
    }
}
