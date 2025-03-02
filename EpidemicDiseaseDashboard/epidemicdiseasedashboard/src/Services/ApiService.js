// src/services/apiService.js
const API_BASE_URL =
  process.env.REACT_APP_API_BASE_URL || "https://localhost:7161/api";

export const fetchYears = async () => {
  try {
    const response = await fetch(`${API_BASE_URL}/EpidemicDiseaseCases/years`);
    return response.json();
  } catch (error) {
    console.error("Error fetching years:", error);
    throw error;
  }
};

export const fetchYearlyData = async () => {
  try {
    const response = await fetch(`${API_BASE_URL}/EpidemicDiseaseCases/yearly`);
    return response.json();
  } catch (error) {
    console.error("Error fetching yearly data:", error);
    throw error;
  }
};

export const fetchWeeklyData = async (year) => {
  try {
    const response = await fetch(
      `${API_BASE_URL}/EpidemicDiseaseCases/year/${year}/weekly`
    );
    return response.json();
  } catch (error) {
    console.error(`Error fetching weekly data for year ${year}:`, error);
    throw error;
  }
};

export const fetchDiseasesByYear = async (year) => {
  try {
    const response = await fetch(
      `${API_BASE_URL}/EpidemicDiseaseCases/year/${year}/diseaseName`
    );
    return response.json();
  } catch (error) {
    console.error(`Error fetching disease data for year ${year}:`, error);
    throw error;
  }
};
export const fetchWeeklyDataForDisease = async (year, disease) => {
  try {
    const response = await fetch(
      `${API_BASE_URL}/EpidemicDiseaseCases/year/${year}/weekly/${disease}`
    );
    return response.json();
  } catch (error) {
    console.error(`Error fetching data for ${disease} in ${year}:`, error);
    throw error;
  }
};


