using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatavendingSite.Models
{
    public class LogEntry
    {
        public long LogEntryId { get; set; }
        public int MachineId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        [DisplayName("Event Time")]
        public DateTime EventTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        [DisplayName("Received Time")]
        public DateTime? ReceivedTime { get; set; }
        
        [Required]
        [DisplayName("Value")]
        public int Category { get; set; }

        public string Description { get; set; }

        public virtual Machine Machine { get; set; }
    }
}