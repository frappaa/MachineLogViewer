using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MachineLogViewer.Models
{
    public class MachineUser : IdentityUser
    {
        public bool IsActive { get; set; }

        public virtual Collection<Machine> Machines { get; set; } 
    }
}