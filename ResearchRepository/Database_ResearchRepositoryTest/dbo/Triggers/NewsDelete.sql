CREATE TRIGGER [NewsDelete]
	ON [dbo].[News]
	INSTEAD OF DELETE
	AS
	BEGIN
		DECLARE @Id int
		SELECT @Id = d.Id
		FROM deleted d

		IF(@Id is NULL)
			BEGIN
				Raiserror('Invalid News ID or News ID not exists', 16, 1)
				RETURN
			END
		ELSE
			UPDATE News SET Deleted = 1 WHERE Id = @Id
	END;