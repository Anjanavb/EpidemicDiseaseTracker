# Epidemic Disease Tracker

## Overview
The Epidemic Disease Tracker project consists of two APIs and a frontend application built with React.js. It allows users to view epidemic disease case data from external sources, processes the data, and displays it in a user-friendly interface.

### Components:
- **EpidemicDiseaseDataApi**: Fetches data from an external government dataset, processes it, and inserts the results into a database.
- **EpidemicDiseaseTrackerAPI**: Fetches processed data from the database and serves it to the frontend React.js app.
- **EpidemicDiseaseDashboard (Frontend)**: A React.js app that displays the epidemic disease case data.

---

## Tech Stack

- **Backend**:
  - **ASP.NET Core 8 (Web API)**:
    - `EpidemicDiseaseDataApi` fetches external data, processes it, and inserts it into MS SQL Server.
    - `EpidemicDiseaseTrackerAPI` serves data to the frontend from MS SQL Server.
    - **Entity Framework** for data access.
    - **NUnit** for unit testing backend logic.
  
- **Frontend**:
  - **React.js** (uses class-based components).
  
- **Version Control**:
  - **Git & GitHub** for version control.

- **Database**:
  - **MS SQL Server** (Developer Edition or equivalent).

---

## Requirements

- **Backend**:
  - .NET SDK 8.0 or higher.
  - MS SQL Server (Developer Edition or equivalent).
  
- **Frontend**:
  - Node.js and npm.
  
---

## How to Run the Project

### Backend (API)

1. **Clone the repository**:
   - Run `git clone <repository-url>` to clone the project to your local machine.

2. **Open the backend project** in Visual Studio.

3. **Restore NuGet Packages**:
   - Right-click the solution in Visual Studio > **Restore NuGet Packages**.

4. **Update the connection string**:
   - In the `appsettings.json` file, update the connection string to point to your local MS SQL Server instance.

5. **Update `EpidemicDiseaseTrackerAPI` URLs and settings**:
   - Open `appsettings.json` in the backend project.
   - Update the API URLs, CORS settings, and other required configurations.

6. **CORS Setup**:
   - Ensure that the `AllowOrigins` setting is updated with the correct frontend URL (e.g., `http://localhost:3000`) to allow `EpidemicDiseaseTrackerAPI` to accept requests from your React.js frontend.

7. **Run the backend project** in Visual Studio:
   - The API will be hosted on the configured URL.
     - Example:
       - `EpidemicDiseaseDataApi`: https://localhost:7041
       - `EpidemicDiseaseTrackerAPI`: https://localhost:7161

### Frontend (React.js)

1. **Open the project `EpidemicDiseaseDashboard`** in your preferred code editor (e.g., VS Code).

2. **Configure React.js to use `EpidemicDiseaseTrackerAPI`**:
   - Open the `.env` file in the React.js project.
   - Set the `REACT_APP_API_BASE_URL` value to match the backend `EpidemicDiseaseTrackerAPI` URL (e.g., `https://localhost:7161/api`).

3. **Install project dependencies**:
   - Run `npm install` in the terminal.

4. **Start the React app**:
   - Run `npm start` to start the React development server.
   - The React app will be available at `http://localhost:3000`.

---

## Features

- **EpidemicDiseaseDataApi**:
  - Fetches external data from government datasets.
  - Processes the data (calculates the number of cases per year and weekly) and inserts the results into the database.

- **EpidemicDiseaseTrackerAPI**:
  - Fetches the processed data from the database and serves it to the frontend.

- **Frontend (`EpidemicDiseaseDashboard`)**:
  - Displays epidemic disease case data.
  - Provides yearly and weekly reports based on user selection.

---

## Notes

- Ensure your local **MS SQL Server** is running and accessible.
- Make sure **EpidemicDiseaseDataApi** is running and has fetched and processed the data before interacting with **EpidemicDiseaseTrackerAPI**.
- The backend **EpidemicDiseaseTrackerAPI** must be running before the frontend can fetch data.

