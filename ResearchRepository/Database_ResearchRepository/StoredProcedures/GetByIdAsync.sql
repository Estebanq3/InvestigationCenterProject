CREATE PROCEDURE [dbo].GetByIdAsync
@Id int
AS
BEGIN
	SELECT *
	FROM dbo.InvestigationProject ip
	WHERE ip.Id = @Id
END