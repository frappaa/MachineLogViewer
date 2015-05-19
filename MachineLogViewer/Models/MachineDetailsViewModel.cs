using System;
using System.Collections.Generic;

namespace MachineLogViewer.Models
{
    public class MachineDetailsViewModel
    {
        public int MachineId { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }

        public List<LogEntry> LogEntries { get; set; }

    }
}