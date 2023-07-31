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
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AuthDb;Data Source=DESKTOP-M9V7B90\\SQLEXPRESS;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
