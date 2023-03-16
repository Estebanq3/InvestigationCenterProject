CREATE TRIGGER [PublicationDelete]
	ON [dbo].[Publication]
	INSTEAD OF DELETE
	AS
	BEGIN

		DECLARE @Id varchar(50)

		SELECT @Id = d.Id
		FROM deleted d

		UPDATE Publication SET Deleted = 1 WHERE Id = @Id
		
	END;