using System;
using System.Collections.Generic;

namespace MachineLogViewer.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual ICollection<LogEntry> LogEntries { get; set; }

    }
}