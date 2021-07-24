using System;

namespace AgendaUoW.Persistence.UoW
{
    using AgendaUoW.Domain.UoW;
    using AgendaUoW.Persistence.Config;
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;
        public UnitOfWork(DbSession _session)
        {
            this._session = _session ?? throw new ArgumentNullException(nameof(_session));
        }

        public void Begintransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();


        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }
    }
}
