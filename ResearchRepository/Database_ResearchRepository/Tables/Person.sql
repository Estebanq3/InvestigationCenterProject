CREATE TABLE [dbo].[Person]
(
	[Email] NVARCHAR(60) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(60) NOT NULL, 
    [FirstLastName] NVARCHAR(30) NOT NULL, 
    [SecondLastName] NVARCHAR(30) NOT NULL, 
    [Country] NVARCHAR(60) NULL DEFAULT 'Desconocido'
)
