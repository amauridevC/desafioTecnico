using System;
using VR.Domain.Interfaces;
using VR.Infra.Data.Context;

namespace VR.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VRContext _context;

        public UnitOfWork(VRContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges("Sistema");
        }

        public int Commit(string usuario = "Sistema")
        {
            return _context.SaveChanges(usuario);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}