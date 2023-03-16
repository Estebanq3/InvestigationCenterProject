CREATE TABLE [dbo].[Investigator] (
    [Email] NVARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([Email] ASC),
    FOREIGN KEY ([Email]) REFERENCES [dbo].[Collaborator] ([Email])
);

