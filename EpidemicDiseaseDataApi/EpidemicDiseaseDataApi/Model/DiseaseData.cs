namespace EpidemicDiseaseDataApi.Model
{
    public class DiseaseData
    {
        public int Id { get; set; }
        public string EpiWeek { get; set; }
        public string Disease { get; set; }
        public int NoOfCases { get; set; }
    }
}
