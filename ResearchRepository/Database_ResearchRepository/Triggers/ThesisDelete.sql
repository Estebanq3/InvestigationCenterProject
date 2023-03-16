
GO
CREATE TRIGGER delete_cascade_thesis
ON Thesis INSTEAD OF DELETE
AS
BEGIN
	DECLARE @Id INT

	DECLARE c CURSOR FOR
		SELECT Id
		FROM deleted d;

	OPEN c;

	FETCH NEXT FROM c INTO @Id
	WHILE @@FETCH_STATUS = 0 BEGIN
		
		BEGIN TRY 
			BEGIN TRANSACTION

				DELETE FROM ThesisPartOfProject
				WHERE ThesisId = @Id;

				DELETE FROM AuthorsPartOfThesis
				WHERE ThesisId = @Id;

				DELETE FROM PublicationPartOfTesis
				WHERE ThesisId = @Id;
				
				DELETE FROM ResearchAreaThesis
				WHERE ThesisId = @Id;

				DELETE FROM Thesis
				WHERE Id = @Id;

			COMMIT TRANSACTION;
		END TRY 
		BEGIN CATCH
			SELECT ERROR_STATE()
			ROLLBACK TRAN 
		END CATCH 

		FETCH NEXT FROM c INTO @Id
	END
	CLOSE c;
	DEALLOCATE c; 
END;
GO
