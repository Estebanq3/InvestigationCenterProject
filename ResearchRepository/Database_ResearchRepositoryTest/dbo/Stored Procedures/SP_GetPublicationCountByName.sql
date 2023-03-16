CREATE PROCEDURE [dbo].[SP_GetPublicationCountByName]
@groupId int,
@publicationName VARCHAR(MAX)
AS
BEGIN
	SELECT COUNT(*) AS VALUE
	FROM dbo.Publication p
	WHERE p.ResearchGroupId = @groupId
	AND LOWER(p.Name) LIKE '%'+LOWER(@publicationName)+'%'
END