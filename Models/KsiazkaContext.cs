using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming_Lesson3.Models
{
    public class KsiazkaContext : DbContext
    {

        public KsiazkaContext(DbContextOptions<KsiazkaContext> options)
            : base(options)
        {
        }

        public DbSet<KsiazkaItem> KsiazkaItems { get; set; }
    }
}
