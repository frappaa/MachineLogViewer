using System;

namespace MachineLogViewer.Models
{
    public enum Category
    {
        A, B, C, D, F
    }

    public class LogEntry
    {
        public long LogEntryId { get; set; }
        public int MachineId { get; set; }
        public DateTime Time { get; set; }
        public Category Category { get; set; }

        public virtual Machine Machine { get; set; }
    }
}