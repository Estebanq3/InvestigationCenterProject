CREATE TABLE [dbo].[ResearchAreaThesis]
(
	[ResearchAreasId] INT NOT NULL,
	[ThesisId] Int NOT NULL,

	PRIMARY KEY (ResearchAreasId,ThesisId),
	FOREIGN KEY (ResearchAreasId) REFERENCES [dbo].[ResearchArea] ([Id]),
	FOREIGN KEY (ThesisId) REFERENCES [dbo].[Thesis] ([Id])
)