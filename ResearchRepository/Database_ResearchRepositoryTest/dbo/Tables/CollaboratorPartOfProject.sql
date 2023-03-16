CREATE TABLE [dbo].[CollaboratorPartOfProject] (
    [CollaboratorEmail]      NVARCHAR (60) NOT NULL,
    [InvestigationProjectId] INT           NOT NULL,
    [Role]                   NCHAR (60)    NOT NULL,
    PRIMARY KEY CLUSTERED ([CollaboratorEmail] ASC, [InvestigationProjectId] ASC),
    FOREIGN KEY ([CollaboratorEmail]) REFERENCES [dbo].[Collaborator] ([Email]) ON DELETE CASCADE,
    FOREIGN KEY ([InvestigationProjectId]) REFERENCES [dbo].[InvestigationProject] ([Id]) ON DELETE CASCADE
);

