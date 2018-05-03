using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        [ForeignKey("CashMachine")]
        public int CashMachineId { get; set; }
        public CashMachine CashMachine { get; set; }
        public int Money { get; set; }
        public DateTime Time { get; set; }
    }
}
