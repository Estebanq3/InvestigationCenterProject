CREATE TABLE [dbo].[NewsPerson]
(
	[AssociatedNewsId] INT NOT NULL,
	[AssociatedPeopleEmail] NVARCHAR(60) NOT NULL,
	PRIMARY KEY ([AssociatedNewsId],[AssociatedPeopleEmail]),
	FOREIGN KEY ([AssociatedNewsId]) REFERENCES [dbo].[News]([Id]),
	FOREIGN KEY ([AssociatedPeopleEmail]) REFERENCES [dbo].[Person]([Email])
)
