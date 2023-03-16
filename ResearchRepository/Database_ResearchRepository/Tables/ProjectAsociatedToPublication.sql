CREATE TABLE [dbo].[ProjectAsociatedToPublication]
(
	[PublicationId] VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Publication(Id), 
    [InvestigationProjectId] INT NOT NULL FOREIGN KEY REFERENCES InvestigationProject(Id),
	PRIMARY KEY ([PublicationId],[InvestigationProjectId]) 
)
