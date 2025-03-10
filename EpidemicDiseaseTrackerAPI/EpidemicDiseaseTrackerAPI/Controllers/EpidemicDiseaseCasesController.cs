using EpidemicDiseaseTrackerAPI.Data;
using EpidemicDiseaseTrackerAPI.Models;
using EpidemicDiseaseTrackerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpidemicDiseaseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpidemicDiseaseCasesController : ControllerBase
    {
        private readonly IEpidemicDiseaseCasesRepository _repository;

        public EpidemicDiseaseCasesController(IEpidemicDiseaseCasesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("yearly")]
        public async Task<IActionResult> GetCasesByYear()
        {
            try
            {
                var cases = await _repository.GetYearlyCasesAsync();
                if (!cases.Any()) return NotFound("No data available");
                return Ok(cases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("year/{year}/weekly")]
        public async Task<IActionResult> GetCasesByYearWeekly(int year)
        {
            try
            {

                var cases = await _repository.GetWeeklyCasesAsync(year);
                if (!cases.Any()) return NotFound("No weekly data found for the given year.");

                return Ok(cases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("years")]
        public async Task<IActionResult>GetAvailableYears()
        {
            try
            {
                var years = await _repository.GetAvailableYearsAsync();
                if (!years.Any()) return NotFound("No years found.");

                return Ok(years);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

        }
        [HttpGet("year/{year}/weekly/{diseaseName}")]
        public async Task<IActionResult> GetWeeklyCasesByDisease(int year, string diseaseName)
        {
            
            if (year < 2012 || year > 2022)
                return BadRequest("Invalid year provided.");
            if (string.IsNullOrWhiteSpace(diseaseName))

                return BadRequest("Disease name cannot be empty.");
            try
            {
                var cases = await _repository.GetWeeklyCasesByDiseaseNameAsync(year, diseaseName);
                if (!cases.Any()) return NotFound("No weekly data found for the given year or disease.");
                return Ok(cases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
        [HttpGet("year/{year}/diseaseName")]
        public async Task<IActionResult> GetDiseasesForYear(int year)
        {
            try
            {
                var diseases = await _repository.GetDiseasesForYearAsync(year);
                if (!diseases.Any()) return NotFound("No diseases found for the given year.");
                return Ok(diseases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
