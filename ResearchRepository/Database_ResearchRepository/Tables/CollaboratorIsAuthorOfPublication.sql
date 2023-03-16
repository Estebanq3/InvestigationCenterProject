CREATE TABLE [dbo].[CollaboratorIsAuthorOfPublication]
(
	[EmailCollaborator] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Collaborator([Email]), 
	 [IdPublication] VARCHAR(50)  NOT NULL FOREIGN KEY REFERENCES Publication([Id]),
	 PRIMARY KEY ([EmailCollaborator],[IdPublication])
)
