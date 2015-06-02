using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MachineLogViewer.Models
{
    public class Machine
    {
        public int MachineId { get; set; }

        [Required]
        [RegularExpression("^[A-Z0-9]{12}$", ErrorMessage = "The {0} must contain exactly 12 alphanumeric characters.")]
        public string Code { get; set; }
        
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        public virtual ICollection<LogEntry> LogEntries { get; set; }

        public virtual ICollection<Takings> Takings { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}