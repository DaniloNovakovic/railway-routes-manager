using System;

namespace Server.Core
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}