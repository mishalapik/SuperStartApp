using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MultiLaunch
{
    class ApplicationContext : DbContext
    {
        public DbSet<Program> Program { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
