using EpidemicDiseaseDataApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EpidemicDiseaseDataApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DiseaseData> DiseaseData { get; set; }
    }
}
