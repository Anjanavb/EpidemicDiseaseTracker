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
        [JsonProperty("epi_week")] // Map to epi_week in JSON
        public string EpiWeek { get; set; }

        [JsonProperty("disease")] // Map to disease in JSON
        public string Disease { get; set; }

        [JsonProperty("no._of_cases")] // Map to no._of_cases in JSON
        public string NoOfCases { get; set; }

        [JsonProperty("_id")] // Map to _id in JSON
        public int Id { get; set; }
    }

}
