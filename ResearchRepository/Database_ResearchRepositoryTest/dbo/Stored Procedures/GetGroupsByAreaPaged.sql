CREATE TYPE [dbo].[ListArea] AS TABLE(
    [AreaId] [INT]
);
GO
CREATE PROCEDURE [dbo].[SP_GetGroupsByAreaPaged]
@centerId int,
@list ListArea READONLY,
@currentPage int,
@size int
AS
BEGIN
	SELECT DISTINCT rg.* 
    FROM dbo.ResearchGroup rg JOIN ResearchAreaResearchGroup ra ON rg.Id = ra.ResearchGroupsId
	WHERE rg.CenterId = @centerId AND ra.ResearchAreasId IN (SELECT l.AreaId FROM @list l) AND Active = 1
	ORDER BY rg.Name DESC	
	OFFSET ((@currentPage - 1) * @size) ROWS
	FETCH NEXT @size ROWS ONLY
END