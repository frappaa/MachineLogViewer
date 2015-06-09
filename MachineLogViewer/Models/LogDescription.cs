using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineLogViewer.Models
{
    public class LogDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Value { get; set; }

        public string Description { get; set; }
    }
}