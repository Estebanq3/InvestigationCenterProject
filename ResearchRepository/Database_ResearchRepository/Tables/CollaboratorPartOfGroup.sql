CREATE TABLE [dbo].[CollaboratorPartOfGroup]
(
	[CollaboratorEmail] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Collaborator(Email), 
    [InvestigationGroupId] INT NOT NULL FOREIGN KEY REFERENCES ResearchGroup(Id),
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL, 
    PRIMARY KEY ([CollaboratorEmail],[InvestigationGroupId])
)
