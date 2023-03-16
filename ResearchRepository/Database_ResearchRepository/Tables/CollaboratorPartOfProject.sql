CREATE TABLE [dbo].[CollaboratorPartOfProject]
(
	[CollaboratorEmail] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Collaborator([Email])On Delete Cascade, 
    [InvestigationProjectId] INT NOT NULL FOREIGN KEY REFERENCES InvestigationProject([Id])On Delete Cascade,
    [Role] NCHAR(60) NOT NULL,
    PRIMARY KEY ([CollaboratorEmail],[InvestigationProjectId])
)