CREATE TABLE [dbo].[ResearchAreaResearchSubArea] (
    [ResearchAreasId]    INT NOT NULL,
    [ResearchSubAreasId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ResearchAreasId] ASC, [ResearchSubAreasId] ASC),
    FOREIGN KEY ([ResearchAreasId]) REFERENCES [dbo].[ResearchArea] ([Id]),
    FOREIGN KEY ([ResearchSubAreasId]) REFERENCES [dbo].[ResearchArea] ([Id])
);

