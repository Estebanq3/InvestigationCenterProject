CREATE PROCEDURE [dbo].[GetIdByNameTheses]
(	
@name varchar(300) = '',
@publicationDate datetime
)
AS
BEGIN
SELECT * from dbo.Thesis t1 WHERE t1.Name = @name and t1.PublicationDate = @publicationDate
END