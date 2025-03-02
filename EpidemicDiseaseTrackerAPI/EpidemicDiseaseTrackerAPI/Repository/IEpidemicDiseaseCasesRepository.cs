using EpidemicDiseaseTrackerAPI.Models;

namespace EpidemicDiseaseTrackerAPI.Repository
{
    public interface IEpidemicDiseaseCasesRepository
    {
        Task<List<DiseaseCasesByYear>> GetYearlyCasesAsync();
        Task<List<DiseaseCasesByWeek>> GetWeeklyCasesAsync(int year);
        Task<List<int>> GetAvailableYearsAsync();
        Task<List<DiseaseData>> GetWeeklyCasesByDiseaseNameAsync(int year, string diseaseName);
        Task<List<string>>GetDiseasesForYearAsync(int year);
    }

}
