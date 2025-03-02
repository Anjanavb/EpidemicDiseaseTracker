using EpidemicDiseaseDataApi.Data;
using EpidemicDiseaseDataApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EpidemicDiseaseDataApi.Repository
{
    public class EpidemicDiseaseDataRepository : IEpidemicDiseaseDataRepository
    {
        private readonly AppDbContext _context;
        private const int Limit = 1000;
        public EpidemicDiseaseDataRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> InsertDiseaseData(List<DiseaseData> diseaseDataList)
        {
            try
            {
                if (diseaseDataList.Count > 0)
                {
                    await _context.DiseaseData.AddRangeAsync(diseaseDataList);
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync("EXEC dbo.CalculateWeeklyCases");
                    await _context.Database.ExecuteSqlRawAsync("EXEC dbo.CalculateYearlyCases");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}