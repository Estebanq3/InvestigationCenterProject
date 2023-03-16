CREATE TABLE [dbo].[Student] (
    [Email]     NVARCHAR (60) NOT NULL,
    [StudentId] NVARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Email] ASC),
    FOREIGN KEY ([Email]) REFERENCES [dbo].[Collaborator] ([Email])
);

