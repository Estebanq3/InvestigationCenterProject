CREATE TABLE [dbo].[ResearchAreaResearchGroup] (
    [ResearchAreasId]  INT NOT NULL,
    [ResearchGroupsId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ResearchAreasId] ASC, [ResearchGroupsId] ASC),
    FOREIGN KEY ([ResearchAreasId]) REFERENCES [dbo].[ResearchArea] ([Id]),
    FOREIGN KEY ([ResearchGroupsId]) REFERENCES [dbo].[ResearchGroup] ([Id])
);

