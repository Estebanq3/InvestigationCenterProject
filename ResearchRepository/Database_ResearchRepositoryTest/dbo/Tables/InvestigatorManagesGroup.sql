CREATE TABLE [dbo].[InvestigatorManagesGroup] (
    [Email]     NVARCHAR (60) NOT NULL,
    [GroupId]   INT           NOT NULL,
    [StartDate] DATE          NULL,
    [EndDate]   DATE          NULL,
    PRIMARY KEY CLUSTERED ([Email] ASC, [GroupId] ASC),
    FOREIGN KEY ([Email]) REFERENCES [dbo].[Investigator] ([Email]),
    FOREIGN KEY ([GroupId]) REFERENCES [dbo].[ResearchGroup] ([Id])
);

