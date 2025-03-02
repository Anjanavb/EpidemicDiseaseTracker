using EpidemicDiseaseDataApi.Model;

namespace EpidemicDiseaseDataApi.Repository
{
    public interface IEpidemicDiseaseApiRepository
    {
        Task<List<DiseaseData>> FetchEpidemicDiseaseDataFromApi();
    }
}
