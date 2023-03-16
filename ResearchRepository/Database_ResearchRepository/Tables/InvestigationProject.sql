CREATE TABLE [dbo].[InvestigationProject]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(200) NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [InvestigationGroupID] INT NULL, 
    [Description] VARCHAR(8000) NULL, 
    [Summary] VARCHAR(8000) NULL, 
    [Image] VARCHAR(MAX) NULL, 
    [Active] BIT NOT NULL DEFAULT 1
)
