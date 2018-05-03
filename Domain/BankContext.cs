namespace Domain
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BankContext : DbContext
    {
        public BankContext()
            : base("name=BankContext")
        {
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Purse> Purses { get; set; }
        public DbSet<CashMachine> CashMachines { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}