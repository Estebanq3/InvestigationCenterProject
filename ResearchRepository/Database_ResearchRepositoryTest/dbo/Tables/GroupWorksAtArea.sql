CREATE TABLE [dbo].[GroupWorksAtArea]
(
	[Id] INT NOT NULL, 
    [AreaId] INT NOT NULL,
	CONSTRAINT [FK_Works_ToGroup] FOREIGN KEY ([Id]) REFERENCES [ResearchGroup]([Id]),
	CONSTRAINT [FK_Works_ToArea] FOREIGN KEY ([AreaId]) REFERENCES [ResearchArea]([Id]),
	CONSTRAINT [PK_GroupWorksAtArea] PRIMARY KEY ([Id],[AreaId])

)
