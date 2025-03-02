using EpidemicDiseaseTrackerAPI.Data;
using EpidemicDiseaseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EpidemicDiseaseTrackerAPI.Repository
{
    public class EpidemicDiseaseCasesRepository : IEpidemicDiseaseCasesRepository
    {
        private readonly EpidemicDbContext _context;

        public EpidemicDiseaseCasesRepository(EpidemicDbContext context)
        {
            _context = context;
        }

        public async Task<List<DiseaseCasesByYear>> GetYearlyCasesAsync()
        {
            return await _context.DiseaseCasesByYears
                .OrderBy(c => c.ReportYear)
                .Select(c => new DiseaseCasesByYear { ReportYear = c.ReportYear, CasesReported = c.CasesReported })
                .ToListAsync();
        }

        public async Task<List<DiseaseCasesByWeek>> GetWeeklyCasesAsync(int year)
        {
            return await _context.DiseaseCasesByWeeks
                .Where(c => c.ReportYear == year)
                .OrderBy(c => c.ReportWeek)
                .Select(c => new DiseaseCasesByWeek { ReportYear = c.ReportYear, ReportWeek = c.ReportWeek, CasesReported = c.CasesReported })
                .ToListAsync();
        }

        public async Task<List<int>> GetAvailableYearsAsync()
        {
            return await _context.DiseaseCasesByYears
                .Select(c => c.ReportYear)
                .Distinct()
                .OrderByDescending(y => y)
                .ToListAsync();
        }
        public async Task<List<DiseaseData>> GetWeeklyCasesByDiseaseNameAsync(int year, string diseaseName)
        {
            return await _context.DiseaseData
                .Where(d => d.EpiWeek != null &&
                            d.EpiWeek.StartsWith(year.ToString()) && 
                            d.Disease == diseaseName)
                .ToListAsync();
        }
        public async Task<List<string>> GetDiseasesForYearAsync(int year)
        {
            return await _context.DiseaseData
                            .Where(d => d.EpiWeek != null && d.EpiWeek.StartsWith(year.ToString()))
                            .Select(d => d.Disease!)
                            .Distinct()
                            .OrderBy(y => y)
                            .ToListAsync();
        }
    }

}
