CREATE TABLE [dbo].[AccountIsAdministratorOfGroup]
(
	[Email] NVARCHAR(60) NOT NULL, 
    [GroupId] INT NOT NULL
	PRIMARY KEY ([Email],[GroupId])
)
