CREATE PROC new_person (
	@firstName varchar(60),
	@firstLastName varchar(30),
	@secondLastName varchar(30),
	@email nvarchar(60),
	@country nvarchar(60)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO Person(FirstName,FirstLastName,SecondLastName,Email,Country)
			values(@firstName,@firstLastName,@secondLastName,@email,@country);
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
END;