CREATE TABLE [dbo].[ResearchArea] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100)  NOT NULL,
    [Description] VARCHAR (8000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_ResearchAreaName]
    ON [dbo].[ResearchArea]([Name] ASC);

