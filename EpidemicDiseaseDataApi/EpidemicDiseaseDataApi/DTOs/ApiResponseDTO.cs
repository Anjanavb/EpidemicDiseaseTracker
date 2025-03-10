using Newtonsoft.Json;

namespace EpidemicDiseaseDataApi.DTOs
{
    public class ApiResponseDTO
    {
        public Result Result { get; set; }
    }

    public class Result
    {
        public List<Record> Records { get; set; }
    }

    public class Record
    {
        [JsonProperty("epi_week")]
        public string EpiWeek { get; set; }

        [JsonProperty("disease")] 
        public string Disease { get; set; }

        [JsonProperty("no._of_cases")] 
        public string NoOfCases { get; set; }

        [JsonProperty("_id")] 
        public int Id { get; set; }
    }

}
