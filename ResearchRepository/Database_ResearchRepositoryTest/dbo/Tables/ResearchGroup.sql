CREATE TABLE [dbo].[ResearchGroup]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Description] VARCHAR(8000) NULL,
    [ImageName] NVARCHAR(MAX) NULL,
    [CreationDate] DATE NULL, 
    [CenterId] INT NOT NULL,
    [Active] BIT NOT NULL DEFAULT 0,
    [Deleted] BIT NOT NULL DEFAULT 0, 
    [AdminEmail] NVARCHAR(60) NOT NULL DEFAULT 'marcelo.jenkins@ucr.ac.cr', 
    CONSTRAINT [FK_Group_ToCenter] FOREIGN KEY ([CenterId]) REFERENCES [ResearchCenter]([Id])
)
