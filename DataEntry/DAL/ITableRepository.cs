using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataEntry.Models;

namespace DataEntry.DAL
{
    public interface ITableRepository : IDisposable
    {
        IEnumerable<Table> GetTable();
        Table GetTableBySTD(DateTime STD);
        void InsertFlight(Table table);

        void DeleteFlight(string id);

        void UpdateFlight(Table table);

        void Save();
    }
}