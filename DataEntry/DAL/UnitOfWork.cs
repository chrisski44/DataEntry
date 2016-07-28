using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataEntry.Models;

namespace DataEntry.DAL
{
    public class UnitOfWork : IDisposable
    {

        private TableContext context = new TableContext();
        private GenericRepository<Table> tableRepository;

        public GenericRepository<Table> TableRepository
        {
            get
            {
                if(this.tableRepository == null)
                {
                    this.tableRepository = new GenericRepository<Table>(context);
                }

                return tableRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}