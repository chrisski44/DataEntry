using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntry.Models
{
    public class TableViewModel
    {
        public IEnumerable<Table> PastTime { get; set; }

        public IEnumerable<Table> CurrentFlight { get; set; }

        public IEnumerable<Table> UpcomingFlight { get; set;}
    }
}