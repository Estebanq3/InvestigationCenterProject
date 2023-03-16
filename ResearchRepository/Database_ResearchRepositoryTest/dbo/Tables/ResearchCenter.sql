CREATE TABLE [dbo].[ResearchCenter] (
    [Id]           INT            NOT NULL,
    [Name]         VARCHAR (200)  NOT NULL,
    [Description]  VARCHAR (8000) NULL,
    [Abbreviation] VARCHAR (10)   NULL,
    [ImageName]    VARCHAR (200)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

