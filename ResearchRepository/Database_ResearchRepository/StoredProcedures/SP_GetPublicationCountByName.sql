CREATE PROCEDURE [dbo].[SP_GetPublicationCountByName]
@groupId int,
@publicationName VARCHAR(MAX)
AS
BEGIN
BEGIN TRY
		BEGIN TRANSACTION
			SELECT COUNT(*) AS VALUE
			FROM dbo.Publication p
			WHERE p.ResearchGroupId = @groupId
			AND LOWER(p.Name) LIKE '%'+LOWER(@publicationName)+'%'
		COMMIT TRANSACTION 
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() 
		ROLLBACK TRANSACTION
	END CATCH
END