using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MachineLogViewer.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        public virtual ICollection<LogEntry> LogEntries { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}