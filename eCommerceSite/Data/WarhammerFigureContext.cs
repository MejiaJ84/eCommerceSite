using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Data
{
    public class WarhammerFigureContext : DbContext
    {
        public WarhammerFigureContext(DbContextOptions<WarhammerFigureContext> options ) : base(options)
        {

        }

        public DbSet<Figure> Figures { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
