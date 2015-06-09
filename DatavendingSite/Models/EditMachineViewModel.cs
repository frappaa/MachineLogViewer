﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DatavendingSite.Models
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
        [RegularExpression("^[A-Za-z0-9]{12}$", ErrorMessage = "The {0} must contain exactly 12 alphanumeric characters.")]
        [Remote("DoesMachineCodeExist", "Machine", AdditionalFields = "MachineId", HttpMethod = "POST", ErrorMessage = "Machine code already exists. Please enter a different machine code.")]
        public string Code { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        public string UserId { get; set; }

        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}