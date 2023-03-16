CREATE PROCEDURE [dbo].GetProjectsCountById
@IdGroup int
AS
BEGIN
	SELECT COUNT(*) as Value FROM dbo.InvestigationProject ip
	WHERE ip.InvestigationGroupID = @IdGroup
END