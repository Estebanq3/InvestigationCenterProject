CREATE PROCEDURE [dbo].[SP_GetPublicationCountByResearchGroup]
@researchGroupId int
AS
BEGIN
	SELECT COUNT(*) AS VALUE
	FROM dbo.Publication p
	WHERE p.ResearchGroupId = @researchGroupId and p.Deleted = 'false'
END