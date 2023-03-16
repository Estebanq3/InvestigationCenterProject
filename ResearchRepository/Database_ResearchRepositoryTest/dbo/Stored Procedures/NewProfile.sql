CREATE PROC new_profile (
	@email nvarchar(60),
	@degree nvarchar(30),
	@biography nvarchar(500),
	@title nvarchar(MAX)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO AcademicProfile(Email,Biography,Degree,Title)
			values (@email,@biography,@degree,@title);
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF (XACT_STATE()) = -1
			BEGIN
				ROLLBACK TRANSACTION;
			END
		IF (XACT_STATE()) = 1
			BEGIN
				COMMIT TRANSACTION
			END
	END CATCH
END
go