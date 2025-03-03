
CREATE PROCEDURE CalculateYearlyCases
AS
BEGIN
  SET NOCOUNT ON;

	   DELETE FROM DiseaseCasesByYear;

    INSERT INTO DiseaseCasesByYear (CasesReported, ReportYear)
    SELECT 
        SUM(NoOfCases) AS CasesReported, 
        CAST(LEFT(EpiWeek, 4) AS INT) AS ReportYear
    FROM DiseaseData
    WHERE NoOfCases IS NOT NULL
    GROUP BY LEFT(EpiWeek, 4);
END;
--execute CalculateYearlyCases

--drop procedure CalculateYearlyCases
