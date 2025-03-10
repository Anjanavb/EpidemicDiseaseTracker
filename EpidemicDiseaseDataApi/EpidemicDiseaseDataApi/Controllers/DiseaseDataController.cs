using EpidemicDiseaseDataApi.Model;
using Microsoft.AspNetCore.Mvc;
using EpidemicDiseaseDataApi.Repository;

[Route("api/[controller]")]
[ApiController]
public class DiseaseDataController : ControllerBase
{
    private readonly IEpidemicDiseaseDataRepository _dataRepository;
    private readonly IEpidemicDiseaseApiRepository _apiRepository;
    public DiseaseDataController(IEpidemicDiseaseDataRepository dataRepository, IEpidemicDiseaseApiRepository apiRepository)
    {
        _dataRepository = dataRepository;
        _apiRepository = apiRepository;
    }

    [HttpPost]
    public async Task<IActionResult> FetchAndInsertData()
    {
        try
        {
            List<DiseaseData> diseaseDataList = await _apiRepository.FetchEpidemicDiseaseDataFromApi();
            if (diseaseDataList == null || diseaseDataList.Count == 0)
            {
                //404 Not Found, If no data found from the external API.
                return NotFound(new { message = "No data found from the external API." });
            }
            bool isSuccess = await _dataRepository.InsertDiseaseData(diseaseDataList);
            if (!isSuccess)
            {
                //500 Internal Server Error, If database insertion fails or an unexpected error occurs.
                return StatusCode(500, new { message = "Failed to insert data into the database." });
            }
            return Ok(new { message = "All data inserted successfully" });
        }
        catch (HttpRequestException ex)
        {
            // 502 Bad Gateway, if external API is down
            return StatusCode(502, new { message = "Failed to fetch data from the external API.", error = ex.Message });
        }
        catch (Exception ex)
        {
            //500 Internal Server Error, If database insertion fails or an unexpected error occurs.
            return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
        }

    }

}
