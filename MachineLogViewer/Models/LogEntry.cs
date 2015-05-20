using System;
using System.ComponentModel.DataAnnotations;

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

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        public DateTime Time { get; set; }
        
        [Required]
        public Category Category { get; set; }

        public virtual Machine Machine { get; set; }
    }
}