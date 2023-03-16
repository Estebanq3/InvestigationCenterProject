CREATE TABLE [dbo].[AccountIsCollaboratorOfGroup]
(
	[Email] NVARCHAR(60) NOT NULL, 
    [GroupId] INT NOT NULL
	PRIMARY KEY ([Email],[GroupId])
)
