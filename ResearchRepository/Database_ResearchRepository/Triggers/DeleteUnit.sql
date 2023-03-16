CREATE TRIGGER DeleteUnit
	ON dbo.PersonWorksForUnit
	AFTER DELETE
	AS
	BEGIN
		DELETE FROM AcademicUnit
		WHERE Name not in (select UnitName from PersonWorksForUnit)	
	END;