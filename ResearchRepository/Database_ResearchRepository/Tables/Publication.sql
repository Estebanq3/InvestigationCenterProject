CREATE TABLE [dbo].[Publication] (
    [Id]                VARCHAR(50)  NOT NULL ,
    [Name]              VARCHAR(500)  NULL,
    [Summary]           VARCHAR(5000) NULL,
    [Year]              DATE         NULL,
    [TypePublication]   VARCHAR(100)  NULL,
    [JournalConference] VARCHAR(250)  NULL,
    [ResearchGroupId]   INT          NULL, 
    [Image]             VARCHAR(MAX) NULL,
    [Deleted] BIT NOT NULL,
    [DocumentPDF] VARCHAR(100) NULL,
    [DocumentPDFAttached] VARBINARY(MAX) NULL
    PRIMARY KEY CLUSTERED ([Id] ASC)
);