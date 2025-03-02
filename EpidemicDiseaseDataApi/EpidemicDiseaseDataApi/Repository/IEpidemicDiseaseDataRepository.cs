using EpidemicDiseaseDataApi.Model;

namespace EpidemicDiseaseDataApi.Repository
{
    public interface IEpidemicDiseaseDataRepository
    {
       Task <bool> InsertDiseaseData(List<DiseaseData> diseaseDataList);
    }
}
