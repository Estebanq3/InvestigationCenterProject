CREATE TRIGGER [MainContact]
	ON Contact
	INSTEAD OF  UPDATE
	AS
--ta feo el trigger sorry xd
IF UPDATE(MainContact)
	BEGIN
	DECLARE @id int, @nombre varchar(200), @main bit, @nid int, @start datetime, @end datetime, @email varchar(8000), @group int,@mgroup int,@bmain bit, @telephone varchar(8)

	SELECT @nid= i.Id, @bmain = i.MainContact, @mgroup = i.GroupId
	FROM inserted i

	DECLARE a CURSOR 
		FOR SELECT c.Id,c.Email,c.Name,c.StartDate,c.EndDate,c.MainContact,c.GroupId,c.Telephone
		FROM Contact c
		WHERE c.GroupId = @mgroup

	OPEN a
	FETCH a INTO @id,@email,@nombre,@start,@end,@main,@group,@telephone

WHILE @@FETCH_STATUS=0
BEGIN 


IF (@nid = @id and @bmain = 1)
	BEGIN
	UPDATE Contact
	SET MainContact = 1
	WHERE Id = @id
	END

ELSE

	IF (@nid = @id and @bmain = 0)
		BEGIN	
		UPDATE Contact
		SET MainContact = 0
		WHERE Id = @id
	END	
	ELSE
	IF @bmain = 1
		BEGIN	
		UPDATE Contact
		SET MainContact = 0
		WHERE Id = @id
	END
	FETCH a INTO @id,@email,@nombre,@start,@end,@main,@group,@telephone

END
ClOSE a
DEALLOCATE a

END
ELSE
	BEGIN
		SELECT @nid = i.Id,@nombre = i.Name, @email = i.Email, @start = i.StartDate, @end = i.EndDate, @group = i.GroupId, @telephone = Telephone
		FROM inserted i
	
		UPDATE Contact
		SET Name = @nombre, Email = @email, StartDate = @start, EndDate = @end, GroupId = @group, Telephone = @telephone
		WHERE Id = @nid
	END
GO