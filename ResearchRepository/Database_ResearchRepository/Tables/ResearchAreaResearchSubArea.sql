CREATE TABLE [dbo].[ResearchAreaResearchSubArea]
(
	[ResearchAreasId] INT NOT NULL,
    [ResearchSubAreasId] INT NOT NULL,

	PRIMARY KEY ([ResearchAreasId],[ResearchSubAreasId]),
	FOREIGN KEY ([ResearchAreasId]) REFERENCES [dbo].[ResearchArea]([Id]),
	FOREIGN KEY ([ResearchSubAreasId]) REFERENCES [dbo].[ResearchArea]([Id])
)

