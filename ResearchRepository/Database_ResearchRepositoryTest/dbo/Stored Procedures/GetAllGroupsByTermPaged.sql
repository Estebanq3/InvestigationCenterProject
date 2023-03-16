CREATE PROCEDURE [dbo].[SP_GetAllGroupsByTermPaged]
@centerId int,
@term nvarchar(MAX),
@currentPage int,
@size int
AS
BEGIN
	SELECT * FROM dbo.ResearchGroup rg
	WHERE rg.CenterId = @centerId AND (LOWER(rg.Name) like '%'+LOWER(@term)+'%' OR LOWER(rg.Description) like '%'+LOWER(@term)+'%')
	ORDER BY rg.CreationDate DESC	
	OFFSET ((@currentPage - 1) * @size) ROWS
	FETCH NEXT @size ROWS ONLY
END
