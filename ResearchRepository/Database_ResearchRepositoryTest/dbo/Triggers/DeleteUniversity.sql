CREATE TRIGGER DeleteUniversity
	ON dbo.University
	INSTEAD OF DELETE
	AS
	BEGIN

		DECLARE @name NVARCHAR(400)

	DECLARE uniCursos CURSOR FOR
		SELECT Name
		FROM deleted d;

	OPEN uniCursos;

	FETCH NEXT FROM uniCursos INTO @name
	WHILE @@FETCH_STATUS = 0 BEGIN

		DELETE FROM PersonsBelongsToUniversity
		WHERE UniversityName = @name;

		DELETE FROM University
		WHERE Name = @name;

		FETCH NEXT FROM uniCursos INTO @name
	END
	CLOSE uniCursos;
	DEALLOCATE uniCursos; 
		
	END;