using System;


namespace VR.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        int Commit(string usuario);
    }
}
