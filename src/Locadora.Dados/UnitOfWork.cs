using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Dados
{
    public class UnitOfWork
    {
        private readonly LocadoraContext _locadoraContext;
        private readonly IDbContextTransaction dbContextTransaction = null;

        public UnitOfWork(LocadoraContext locadoraContext)
        {
            _locadoraContext = locadoraContext;
        }

        public void IniciarTransacao() 
        {
            if(_locadoraContext.Database.CurrentTransaction != null) 
            {
                _locadoraContext.Database.BeginTransaction();
            }
        }

        public void Commit() 
        {
            _locadoraContext.SaveChanges();
            if (dbContextTransaction != null)
                dbContextTransaction.Commit();
        }

        public void Rollback() 
        {
            
            if (dbContextTransaction != null)
                dbContextTransaction.Rollback();
        }
    }
}
