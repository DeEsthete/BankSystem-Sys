using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Bank
    {
        //[Key]
        //public int Id { get; set; }
        public string BankName { get; set; }
        public IEnumerable<Person> People { get; set; }
        public IEnumerable<CashMachine> CashMachines { get; set; }
        public IEnumerable<History> Histories { get; set; }
    }
}
