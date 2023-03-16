CREATE TABLE [dbo].[Collaborator] (
    [Email] NVARCHAR (60) NOT NULL,
    [Role]  NVARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([Email] ASC),
    FOREIGN KEY ([Email]) REFERENCES [dbo].[Person] ([Email])
);

