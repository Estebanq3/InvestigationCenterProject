CREATE PROCEDURE [dbo].[SP_GetPublicationByThreeFilters]
@groupId int,
@searchTerm VARCHAR(MAX)
AS
BEGIN
	SELECT *
	FROM dbo.Publication p
	WHERE p.ResearchGroupId = @groupId
	AND p.Deleted = 0
	AND ( 
		LOWER(p.Name) LIKE '%'+LOWER(@searchTerm)+'%'
	OR	LOWER(p.Summary) LIKE '%'+LOWER(@searchTerm)+'%'
	OR	LOWER(p.Id) = LOWER(@searchTerm)
	)
END