CREATE TABLE [dbo].[PersonsBelongsToUniversity]
(
	[PersonEmail] NVARCHAR(60) NOT NULL FOREIGN KEY REFERENCES Person(Email), 
    [UniversityName] NVARCHAR(400) NOT NULL FOREIGN KEY REFERENCES University(Name),
	PRIMARY KEY ([PersonEmail],[UniversityName])
)
