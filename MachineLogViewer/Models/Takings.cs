using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MachineLogViewer.Models
{
    public class Takings
    {
        public long TakingsId { get; set; }
        public int MachineId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        [DisplayName("Event Time")]
        public DateTime Date { get; set; }

        public virtual Machine Machine { get; set; }

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; }

        public decimal SumTotal { get; set; }

        public decimal SumCash { get; set; }

        public decimal SumCashless { get; set; }

        public decimal SumProduct1 { get; set; }

        public decimal SumProduct2 { get; set; }

        public decimal SumProduct3 { get; set; }

        public decimal SumProduct4 { get; set; }

        public decimal SumProduct5 { get; set; }

        public decimal SumProduct6 { get; set; }

        public decimal SumProduct7 { get; set; }

        public decimal SumProduct8 { get; set; }

        public decimal SumProduct9 { get; set; }

        public decimal SumProduct10 { get; set; }

        public decimal SumProduct11 { get; set; }

        public decimal SumProduct12 { get; set; }
    }
}