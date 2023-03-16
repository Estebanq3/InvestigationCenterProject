CREATE TABLE [dbo].[PublicationPartOfTesis] (
    [PublicationId] VARCHAR (50) NOT NULL,
    [ThesisId]      INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([PublicationId] ASC, [ThesisId] ASC),
    FOREIGN KEY ([PublicationId]) REFERENCES [dbo].[Publication] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([ThesisId]) REFERENCES [dbo].[Thesis] ([Id]) ON DELETE CASCADE
);

