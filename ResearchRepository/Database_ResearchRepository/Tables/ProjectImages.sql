CREATE TABLE [dbo].[ProjectImages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Image] VARCHAR(MAX) NOT NULL, 
    [ProjectId] int NOT NULL
)
