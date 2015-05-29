using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MachineLogViewer.Models
{
    public class EditMachineViewModel
    {
        public EditMachineViewModel()
        {
            UserId = String.Empty;
            Description = String.Empty;
        }

        public int MachineId { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        public string UserId { get; set; }

        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}