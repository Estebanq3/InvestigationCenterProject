CREATE TABLE [dbo].[ResearchAreaPublication] (
    [ResearchAreasId] INT          NOT NULL,
    [PublicationsId]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ResearchAreasId] ASC, [PublicationsId] ASC),
    FOREIGN KEY ([PublicationsId]) REFERENCES [dbo].[Publication] ([Id]),
    FOREIGN KEY ([ResearchAreasId]) REFERENCES [dbo].[ResearchArea] ([Id])
);

