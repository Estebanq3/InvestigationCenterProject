CREATE TABLE [dbo].[AuthorsPartOfThesis] (
    [CollaboratorEmail] NVARCHAR (60) NOT NULL,
    [ThesisId]          INT           NOT NULL,
    [Role]              NCHAR (60)    NOT NULL,
    PRIMARY KEY CLUSTERED ([CollaboratorEmail] ASC, [ThesisId] ASC),
    FOREIGN KEY ([CollaboratorEmail]) REFERENCES [dbo].[Collaborator] ([Email]) ON DELETE CASCADE,
    FOREIGN KEY ([ThesisId]) REFERENCES [dbo].[Thesis] ([Id]) ON DELETE CASCADE
);

