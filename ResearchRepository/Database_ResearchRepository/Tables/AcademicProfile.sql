CREATE TABLE [dbo].[AcademicProfile]
(
	[Email] NVARCHAR(60) NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Person([Email]), 
    [Biography] NVARCHAR(500) NULL, 
    [ProfilePic] VARCHAR(MAX) NULL, 
    [Degree] NVARCHAR(30) NULL , 
    [FacebookLink] NVARCHAR(MAX) NULL, 
    [GitHubLink] NVARCHAR(MAX) NULL, 
    [LinkedInLink] NVARCHAR(MAX) NULL, 
    [Title] NVARCHAR(MAX) NULL, 
    [Tel] NVARCHAR(25) NULL
)
