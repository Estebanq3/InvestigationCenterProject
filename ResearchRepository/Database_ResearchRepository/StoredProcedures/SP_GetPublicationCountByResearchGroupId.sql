CREATE PROCEDURE [dbo].[SP_GetPublicationCountByResearchGroupId]
@researchGroupId int
AS
BEGIN
	SELECT COUNT(*) AS VALUE
	FROM dbo.Publication p
	WHERE p.ResearchGroupId = @researchGroupId
END