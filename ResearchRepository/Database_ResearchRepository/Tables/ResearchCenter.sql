CREATE TABLE [dbo].[ResearchCenter]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(200) NOT NULL, 
    [Description] VARCHAR(8000) NULL,
    [Abbreviation] VARCHAR(10) NULL,
    [ImageName] NVARCHAR(MAX) NULL
)