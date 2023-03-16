CREATE TABLE [dbo].[CollaboratorPartOfGroup] (
    [CollaboratorEmail]    NVARCHAR (60) NOT NULL,
    [InvestigationGroupId] INT           NOT NULL,
    [StartDate]            DATE          NULL,
    [EndDate]              DATE          NULL,
    PRIMARY KEY CLUSTERED ([CollaboratorEmail] ASC, [InvestigationGroupId] ASC),
    FOREIGN KEY ([CollaboratorEmail]) REFERENCES [dbo].[Collaborator] ([Email]),
    FOREIGN KEY ([InvestigationGroupId]) REFERENCES [dbo].[ResearchGroup] ([Id])
);

