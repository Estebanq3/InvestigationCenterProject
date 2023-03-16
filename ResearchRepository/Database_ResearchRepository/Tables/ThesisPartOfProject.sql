CREATE TABLE [dbo].[ThesisPartOfProject]
(
	[InvestigationProjectId] INT NOT NULL FOREIGN KEY REFERENCES InvestigationProject(Id), 
    [ThesisId] INT NOT NULL FOREIGN KEY REFERENCES Thesis(Id), 
    PRIMARY KEY ([InvestigationProjectId],[ThesisId])
)