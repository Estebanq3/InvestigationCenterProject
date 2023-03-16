CREATE TABLE [dbo].[Subscriptions]
(
	[UserEmail] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Person(Email), 
    [GroupID] INT NOT NULL FOREIGN KEY REFERENCES ResearchGroup(Id), 
    PRIMARY KEY ([UserEmail],[GroupID])
)
