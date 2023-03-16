CREATE TRIGGER [ContactDelete]
	ON [dbo].[Contact]
	INSTEAD OF DELETE
	AS
	BEGIN
		DECLARE @Id int
		SELECT @Id = d.Id
		FROM deleted d

		IF(@Id is NULL)
			BEGIN
				Raiserror('Invalid Contact ID or Contact ID not exists', 16, 1)
				RETURN
			END
		ELSE
			UPDATE Contact SET Deleted = 1 WHERE Id = @Id
	END;