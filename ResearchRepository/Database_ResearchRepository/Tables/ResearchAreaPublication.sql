CREATE TABLE [dbo].[ResearchAreaPublication]
(
	[ResearchAreasId] INT NOT NULL,
	PublicationsId VARCHAR(50) NOT NULL,

	PRIMARY KEY ([ResearchAreasId],PublicationsId),
	FOREIGN KEY ([ResearchAreasId]) REFERENCES [dbo].[ResearchArea] ([Id]),
	FOREIGN KEY (PublicationsId) REFERENCES [dbo].[Publication] ([Id])
)
