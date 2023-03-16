
GO
CREATE TRIGGER delete_cascade
ON InvestigationProject INSTEAD OF DELETE
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
				WHERE InvestigationProjectId = @Id;

				DELETE FROM CollaboratorPartOfProject
				WHERE InvestigationProjectId = @Id;

				DELETE FROM ProjectAsociatedToPublication
				WHERE InvestigationProjectId = @Id;

				DELETE FROM InvestigationProject
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
