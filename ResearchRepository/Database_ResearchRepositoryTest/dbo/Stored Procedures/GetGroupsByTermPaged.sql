CREATE PROCEDURE [dbo].[SP_GetGroupsByTermPaged]
@centerId int,
@term nvarchar(MAX),
@currentPage int,
@size int
AS
BEGIN
	SELECT * FROM dbo.ResearchGroup rg
	WHERE rg.CenterId = @centerId AND (LOWER(rg.Name) like '%'+LOWER(@term)+'%' OR LOWER(rg.Description) like '%'+LOWER(@term)+'%') AND rg.Active = 1
	ORDER BY rg.Name DESC	
	OFFSET ((@currentPage - 1) * @size) ROWS
	FETCH NEXT @size ROWS ONLY
END
