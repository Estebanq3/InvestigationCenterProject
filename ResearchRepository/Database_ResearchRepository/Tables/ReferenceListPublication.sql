CREATE TABLE [dbo].[ReferenceListPublication]
(
	 [IdPublication] VARCHAR(50)  NOT NULL FOREIGN KEY REFERENCES Publication([Id]),
	 [Order] INT NOT NULL,
	 [Reference] VARCHAR(500) NOT NULL 
	 PRIMARY KEY ([IdPublication],[Order],[Reference])
)
