
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

		DELETE FROM ThesisPartOfProject
		WHERE InvestigationProjectId = @Id;

		DELETE FROM CollaboratorPartOfProject
		WHERE InvestigationProjectId = @Id;

		DELETE FROM ProjectAsociatedToPublication
		WHERE InvestigationProjectId = @Id;

		DELETE FROM InvestigationProject
		WHERE Id = @Id;

		FETCH NEXT FROM c INTO @Id
	END
	CLOSE c;
	DEALLOCATE c; 
END;
GO
