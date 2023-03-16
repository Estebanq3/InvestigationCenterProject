CREATE TABLE [dbo].[ProjectAsociatedToPublication] (
    [PublicationId]          VARCHAR (50) NOT NULL,
    [InvestigationProjectId] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([PublicationId] ASC, [InvestigationProjectId] ASC),
    FOREIGN KEY ([InvestigationProjectId]) REFERENCES [dbo].[InvestigationProject] ([Id]),
    FOREIGN KEY ([PublicationId]) REFERENCES [dbo].[Publication] ([Id])
);

