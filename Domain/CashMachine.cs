using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CashMachine
    {
        [Key]
        public int Id { get; set; }
        public string Addres { get; set; }
        public int Money { get; set; }
    }
}
