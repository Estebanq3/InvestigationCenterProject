
CREATE TRIGGER [FirstContact]
	ON Contact
	AFTER INSERT
	AS

	BEGIN
	DECLARE @id int, @nombre varchar(200), @start datetime, @end datetime, @email varchar(8000), @group int, @telephone varchar(8),@contacts int
	
	SELECT @id = i.Id,@nombre = i.Name, @email = i.Email, @start = i.StartDate, @end = i.EndDate, @group = i.GroupId, @telephone = i.Telephone
	FROM inserted i

	SELECT @contacts = COUNT(c.Id)
	FROM Contact c
	WHERE c.GroupId = @group and c.Deleted != 1

	IF @contacts !=  1
		UPDATE Contact
		SET Name = @nombre, Email = @email, StartDate = @start, EndDate = @end, GroupId = @group, Telephone = @telephone, MainContact = 0
		WHERE Id = @id
	ELSE 
		UPDATE Contact
		SET Name = @nombre, Email = @email, StartDate = @start, EndDate = @end, GroupId = @group, Telephone = @telephone, MainContact = 1
		WHERE Id = @id
	END

