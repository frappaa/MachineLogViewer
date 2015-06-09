using System;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace DatavendingSite.Models
{
    public class LogViewModel
    {
        public int MachineId { get; set; }

        public string Code { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        public IPagedList<LogEntry> LogEntries { get; set; }

    }
}