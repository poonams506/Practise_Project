using MebershipApI.Moduels.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MebershipApI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Members> MemberList { get; set; }
    }
}
