using EpidemicDiseaseDataApi.DTOs;
using EpidemicDiseaseDataApi.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EpidemicDiseaseDataApi.Repository
{
    public class EpidemicDiseaseApiRepository : IEpidemicDiseaseApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private const int Limit = 1000;
        public EpidemicDiseaseApiRepository( HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<List<DiseaseData>> FetchEpidemicDiseaseDataFromApi()
        {
            List<DiseaseData> diseaseDataList = new List<DiseaseData>();
            int offset = 0;
            bool hasMoreData = true;
            while (hasMoreData)
            {
                var url = $"{_apiSettings.BaseUrl}?resource_id={_apiSettings.DatasetId}&limit={Limit}&offset={offset}";
                var response = await _httpClient.GetStringAsync(url);

                // Deserialize the jason data
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseDTO>(response);

                if (apiResponse?.Result?.Records == null || apiResponse.Result.Records.Count == 0)
                {
                    hasMoreData = false;
                    break;
                }

                foreach (var record in apiResponse.Result.Records)
                {
                    Console.WriteLine($"EpiWeek: {record.EpiWeek}, Disease: {record.Disease}, NoOfCases: {record.NoOfCases}");
                    diseaseDataList.Add(new DiseaseData
                    {
                        Id = record.Id,
                        EpiWeek = record.EpiWeek,
                        Disease = record.Disease,
                        NoOfCases = int.Parse(record.NoOfCases)
                    });
                }
                offset += Limit;
            }
            return diseaseDataList;
        }

    }
}