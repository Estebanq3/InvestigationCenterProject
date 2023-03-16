CREATE TABLE [dbo].[ReferenceListPublication] (
    [IdPublication] VARCHAR (50)  NOT NULL,
    [Order]         INT           NOT NULL,
    [Reference]     VARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPublication] ASC, [Order] ASC, [Reference] ASC),
    FOREIGN KEY ([IdPublication]) REFERENCES [dbo].[Publication] ([Id])
);

