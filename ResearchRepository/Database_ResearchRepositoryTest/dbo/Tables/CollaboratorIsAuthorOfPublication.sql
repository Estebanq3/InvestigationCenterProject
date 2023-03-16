CREATE TABLE [dbo].[CollaboratorIsAuthorOfPublication] (
    [EmailCollaborator] NVARCHAR (60) NOT NULL,
    [IdPublication]     VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([EmailCollaborator] ASC, [IdPublication] ASC),
    FOREIGN KEY ([EmailCollaborator]) REFERENCES [dbo].[Collaborator] ([Email]),
    FOREIGN KEY ([IdPublication]) REFERENCES [dbo].[Publication] ([Id])
);

