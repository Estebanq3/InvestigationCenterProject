CREATE PROCEDURE [dbo].[SP_AddMainImageToNews]
	@image int,
	@news int
AS
BEGIN
	UPDATE [dbo].[News]
	SET MainImageId = @image
	WHERE Id = @news
END
