CREATE PROCEDURE [dbo].[SP_GetGroupsByAreaAndTermCount]
@centerId int,
@term nvarchar(MAX),
@list ListArea READONLY
AS
BEGIN
	SELECT COUNT(DISTINCT rg.Name) AS VALUE
    FROM dbo.ResearchGroup rg JOIN ResearchAreaResearchGroup ra ON rg.Id = ra.ResearchGroupsId
	WHERE rg.CenterId = @centerId AND ra.ResearchAreasId IN (SELECT l.AreaId FROM @list l) AND Active = 1 AND (LOWER(rg.Name) like '%'+LOWER(@term)+'%' OR LOWER(rg.Description) like '%'+LOWER(@term)+'%')
END