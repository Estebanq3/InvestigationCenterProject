CREATE TABLE [dbo].[Investigator]
(
	[Email] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Collaborator(Email),
	PRIMARY KEY([Email])
)
