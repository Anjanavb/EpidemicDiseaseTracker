import React, { Component } from "react";
import "./Dashboard.css";
import { Bar } from "react-chartjs-2";
import {
  fetchYears,
  fetchYearlyData,
  fetchWeeklyData,
  fetchDiseasesByYear,
  fetchWeeklyDataForDisease,
} from "../../Services/ApiService"; // Import API functions
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
  Scale,
} from "chart.js";

// Register Chart.js components
ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export class Dashboard extends Component {
  state = {
    reporttype: "Yearly",
    years: [],
    selectedYear: "",
    diseases: [],
    selectedDisease: "All",
    yearlyChartData: { labels: [], datasets: [] },
    weeklyChartData: { labels: [], datasets: [] },
    yearlyChartOptions: {
      responsive: true,
      plugins: {
        legend: {
          display: true,
          position: "top",
        },
      },
      scales: {
        x: {
          title: {
            display: true,
            text: "Year",
          },
        },
        y: {
          title: {
            display: true,
            text: "Cases Reported",
          },
          ticks: {
            beginAtZero: true,
          },
        },
      },
    },
    weeklyChartOptions: {
      responsive: true,
      plugins: {
        legend: {
          display: true,
          position: "top",
        },
      },
      scales: {
        x: {
          title: {
            display: true,
            text: "Week",
          },
        },
        y: {
          title: {
            display: true,
            text: "Cases Reported",
          },
          ticks: {
            beginAtZero: true,
          },
        },
      },
    },
    error: "",
    searchDisease: "",
  };

  async componentDidMount() {
    await this.loadYearlyData();
  }

  loadYearlyData = async () => {
    try {
      const data = await fetchYearlyData();
      if (!data || data.length === 0) {
        throw new Error("No yearly data available.");
      }

      const years = data.map((item) => item.reportYear);
      const casesReported = data.map((item) => item.casesReported);

      this.setState({
        yearlyChartData: {
          labels: years,
          datasets: [
            {
              label: "Cases Reported",
              data: casesReported,
              backgroundColor: "rgba(75, 192, 192, 0.5)",
              borderColor: "rgba(75, 192, 192, 1)",
              borderWidth: 1,
            },
          ],
        },
        error: "",
      });
    } catch (error) {
      this.setState({ error: "An error occurred while fetching yearly data." });
    }
  };

  handleReportTypeChange = async (event) => {
    const selectedReport = event.target.value;
    this.setState({ reporttype: selectedReport, error: "" });

    if (selectedReport === "Weekly") {
      try {
        const years = await fetchYears();
        if (!years || years.length === 0) {
          throw new Error("No years available.");
        }
        const latestYear = Math.max(...years);

        this.setState({
          years,
          selectedYear: latestYear,
          selectedDisease: "All",
        });

        await this.loadDiseases(latestYear);
        await this.loadWeeklyData(latestYear);
      } catch (error) {
        this.setState({ error: "An error occurred while fetching years." });
      }
    } else {
      await this.loadYearlyData();
    }
  };

  loadWeeklyData = async (year, disease = "All") => {
    try {
      let data;
      let weeks = [];
      let cases = [];

      if (disease === "All") {
        data = await fetchWeeklyData(year);
        if (!data || data.length === 0) {
          throw new Error("No weekly data available.");
        }
        weeks = data.map((item) => item.reportWeek);
        cases = data.map((item) => item.casesReported);
      } else {
        data = await fetchWeeklyDataForDisease(year, disease);
        if (!data || data.length === 0) {
          throw new Error("No weekly data available.");
        }
        weeks = data.map((item) => item.epiWeek);
        cases = data.map((item) => item.noOfCases);
      }

      this.setState({
        weeklyChartData: {
          labels: weeks.map((week) => `W${week.slice(-2)}`),
          datasets: [
            {
              label: `Cases in ${year} ${disease !== "All" ? `(${disease})` : ""}`,
              data: cases,
              backgroundColor: "rgba(255, 99, 132, 0.5)",
              borderColor: "rgba(255, 99, 132, 1)",
              borderWidth: 1,
            },
          ],
        },
        error: "",
      });
    } catch (error) {
      this.setState({ error: "An error occurred. Please try later." });
    }
  };

  loadDiseases = async (year) => {
    try {
      const diseases = await fetchDiseasesByYear(year);
      this.setState({ diseases, selectedDisease: "All" });
    } catch (error) {
      this.setState({ error: "Failed to fetch diseases." });
    }
  };

  handleYearChange = async (event) => {
    const selectedYear = event.target.value;
    this.setState({ selectedYear, selectedDisease: "All" });
    await this.loadDiseases(selectedYear);
    await this.loadWeeklyData(selectedYear);
  };

  handleDiseaseChange = async (event) => {
    const selectedDisease = event.target.value;
    this.setState({ selectedDisease });
    await this.loadWeeklyData(this.state.selectedYear, selectedDisease);
  };

  handleDiseaseSearch = async (event) => {
    const searchDisease = event.target.value;
    this.setState({ searchDisease });
  };

  handleRetry = async () => {
    this.setState({ error: "" });
    if (this.state.reporttype === "Yearly") {
      await this.loadYearlyData();
    } else {
      await this.handleReportTypeChange({ target: { value: "Weekly" } });
    }
  };

  render() {
    return (
      <div className="dashboard-container">
        <h1>Epidemic Disease Dashboard</h1>

        <div className="input-section">
          <label>
            <input
              type="radio"
              value="Yearly"
              name="reporttype"
              checked={this.state.reporttype === "Yearly"}
              onChange={this.handleReportTypeChange}
            />
            Yearly
          </label>
          <label>
            <input
              type="radio"
              value="Weekly"
              name="reporttype"
              checked={this.state.reporttype === "Weekly"}
              onChange={this.handleReportTypeChange}
            />
            Weekly
          </label>
        </div>

        <div className="fixed-dropdown-section">
          {this.state.reporttype === "Weekly" && this.state.years.length > 0 && (
            <div className="dropdown-section">
              <label>Select Year: </label>
              <select value={this.state.selectedYear} onChange={this.handleYearChange}>
                {this.state.years.map((year) => (
                  <option key={year} value={year}>
                    {year}
                  </option>
                ))}
              </select>

              {this.state.selectedYear && this.state.diseases.length > 0 && (
                <div className="dropdown-section">
                  <input
                    type="text"
                    placeholder="Search Disease"
                    value={this.state.searchDisease}
                    onChange={this.handleDiseaseSearch}
                  />
                  <label>Select Disease: </label>
                  <select value={this.state.selectedDisease} onChange={this.handleDiseaseChange}>
                    <option value="All">All</option>
                    {this.state.diseases
                      .filter((disease) =>
                        disease.toLowerCase().includes(this.state.searchDisease.toLowerCase())
                      )
                      .map((disease) => (
                        <option key={disease} value={disease}>
                          {disease}
                        </option>
                      ))}
                  </select>
                </div>
              )}
            </div>
          )}
        </div>

        {/* Chart Area */}
        <div className="chart-area">
          {this.state.error ? (
            <div className="error-message">
              <span>⚠️ Oops! Something went wrong.</span>
              <p>We couldn't fetch the data. Please try again later.</p>
              <button onClick={this.handleRetry} className="retry-button">
                Retry
              </button>
            </div>
          ) : (
            this.state.yearlyChartData.labels.length > 0 && (
              <Bar
                data={this.state.reporttype === "Yearly" ? this.state.yearlyChartData : this.state.weeklyChartData}
                options={this.state.reporttype === "Yearly" ? this.state.yearlyChartOptions : this.state.weeklyChartOptions}
              />
            )
          )}
        </div>
      </div>
    );
  }
}

export default Dashboard;
