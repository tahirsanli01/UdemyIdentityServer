using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyIdentityServer.Database.Contexts
{
    public partial class AuthDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#if(Debug)
            //optionsBuilder.UseSqlServer("Data Source=192.168.2.108;Initial Catalog=AuthDb;Persist Security Info=True;User ID=sa;Password=saw;TrustServerCertificate=True");
            //#else
            //optionsBuilder.UseSqlServer("Persist Security Info=False;Initial Catalog=AuthDb;Data Source=176.235.236.6; User ID=sa; Password=2656_Tahir;TrustServerCertificate=True;Encrypt=True");
            //#endif
            optionsBuilder.UseSqlServer("Persist Security Info=False;Initial Catalog=AuthDb;Data Source=176.235.236.6; User ID=sa; Password=2656_Tahir;TrustServerCertificate=True;Encrypt=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
