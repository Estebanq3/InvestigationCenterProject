CREATE TABLE [dbo].[ThesisPartOfProject] (
    [InvestigationProjectId] INT NOT NULL,
    [ThesisId]               INT NOT NULL,
    PRIMARY KEY CLUSTERED ([InvestigationProjectId] ASC, [ThesisId] ASC),
    FOREIGN KEY ([InvestigationProjectId]) REFERENCES [dbo].[InvestigationProject] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([ThesisId]) REFERENCES [dbo].[Thesis] ([Id]) ON DELETE CASCADE
);

