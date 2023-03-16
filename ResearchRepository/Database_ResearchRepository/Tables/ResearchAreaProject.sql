CREATE TABLE [dbo].[ResearchAreaProject]
(
	[ResearchAreasId] INT NOT NULL FOREIGN KEY REFERENCES ResearchArea(Id),
	[ProjectsId] INT NOT NULL FOREIGN KEY REFERENCES InvestigationProject (Id),
	PRIMARY KEY ([ResearchAreasId],[ProjectsId]),
)
