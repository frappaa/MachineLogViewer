using System;
using System.Collections.Generic;
using PagedList;

namespace MachineLogViewer.Models
{
    public class MachineDetailsViewModel
    {
        public int MachineId { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }

        public IPagedList<LogEntry> LogEntries { get; set; }

    }
}