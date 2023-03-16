CREATE PROCEDURE [dbo].[SP_GetAllGroupsByTermCount]
@centerId int,
@term nvarchar(MAX)
AS
BEGIN
	SELECT COUNT(*) as Value FROM dbo.ResearchGroup rg
	WHERE rg.CenterId = @centerId AND (LOWER(rg.Name) like '%'+LOWER(@term)+'%' OR LOWER(rg.Description) like '%'+LOWER(@term)+'%')
END