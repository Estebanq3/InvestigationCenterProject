CREATE TABLE [dbo].[InvestigatorManagesGroup]
(
	[Email] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Investigator([Email]), 
    [GroupId] INT NOT NULL FOREIGN KEY REFERENCES ResearchGroup([Id]),
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL
    PRIMARY KEY ([Email],[GroupId])
)
