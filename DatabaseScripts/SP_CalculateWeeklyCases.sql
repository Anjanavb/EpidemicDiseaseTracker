USE [EpidemicDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CalculateWeeklyCases]
AS
BEGIN
    SET NOCOUNT ON;

	  DELETE FROM DiseaseCasesByWeek;
    INSERT INTO DiseaseCasesByWeek (CasesReported, ReportYear, ReportWeek)
    SELECT 
        SUM(NoOfCases) AS CasesReported, 
        CAST(LEFT(EpiWeek, 4) AS INT) AS ReportYear, 
        RIGHT(EpiWeek, 3) AS ReportWeek  
    FROM DiseaseData
    WHERE NoOfCases IS NOT NULL 
    GROUP BY LEFT(EpiWeek, 4), RIGHT(EpiWeek, 3);
END;

--execute CalculateWeeklyCases

--DROP Procedure CalculateWeeklyCases
--select * from DiseaseCasesByWeek order by reportyear,reportweek