using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() :base("name=DefaultConnection")
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}
