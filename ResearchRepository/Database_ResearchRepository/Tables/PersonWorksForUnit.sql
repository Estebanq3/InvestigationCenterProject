CREATE TABLE [dbo].[PersonWorksForUnit]
(
	[Email] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Person(Email), 
    [UnitName] NVARCHAR(200) NOT NULL FOREIGN KEY REFERENCES AcademicUnit(Name),
	PRIMARY KEY ([Email],[UnitName])
)
