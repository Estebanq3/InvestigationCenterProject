CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [Email] VARCHAR(8000) NOT NULL,
    [Telephone] VARCHAR(8) NULL,
    [GroupId] INT NULL,
    [MainContact] BIT NOT NULL DEFAULT 0,
    [Deleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Contact_ToGroup] FOREIGN KEY([GroupId]) REFERENCES [ResearchGroup]([Id]),
)  
