CREATE TABLE [dbo].[News]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(200) NOT NULL, 
    [MainImageId] INT NULL,
    [PublicationDate] DATETIME NOT NULL, 
    [CreationDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [Description] VARCHAR(8000) NOT NULL, 
    [GroupId] INT NULL
    CONSTRAINT [FK_News_ToGroup] FOREIGN KEY([GroupId]) REFERENCES [ResearchGroup]([Id]), 
    [DocumentURN] VARCHAR(100) NULL, 
    [VideoURL] VARCHAR(100) NULL, 
    [Deleted] BIT NOT NULL DEFAULT 0,

    CONSTRAINT [FK_MainImage] FOREIGN KEY ([MainImageId]) REFERENCES [NewsImage]([Id])
)  
