using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace MachineLogViewer.Models
{
    public class MachineDetailsViewModel
    {
        public int MachineId { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        public IPagedList<LogEntry> LogEntries { get; set; }

    }
}