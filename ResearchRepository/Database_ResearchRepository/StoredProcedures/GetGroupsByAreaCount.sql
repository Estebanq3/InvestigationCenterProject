CREATE PROCEDURE [dbo].[SP_GetGroupsByAreaCount]
@centerId int,
@list ListArea READONLY
AS
BEGIN
	SELECT COUNT(DISTINCT rg.Name) AS VALUE
    FROM dbo.ResearchGroup rg JOIN ResearchAreaResearchGroup ra ON rg.Id = ra.ResearchGroupsId
	WHERE rg.CenterId = @centerId AND ra.ResearchAreasId IN (SELECT l.AreaId FROM @list l) AND Active = 1
END