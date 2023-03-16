CREATE TABLE [dbo].[PublicationPartOfTesis]
(
	[PublicationId] VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Publication(Id), 
    [ThesisId] INT NOT NULL FOREIGN KEY REFERENCES Thesis(Id),
	PRIMARY KEY ([PublicationId],[ThesisId])
)
