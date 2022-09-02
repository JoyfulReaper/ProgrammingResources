CREATE PROCEDURE [dbo].[spResource_GetResourceByTag]
	@TagId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		[r].[ResourceId]
		,[r].[Title]
		,[r].[Url]
		,[r].[Description]
		,[r].[ProgrammingLanguage]
		,[r].[DateCreated]
	FROM
		[ResourceTag] rt INNER JOIN
		[Resource] r ON r.ResourceId = rt.ResourceId
	WHERE
		rt.TagId = @TagId
	AND r.DateDeleted IS NULL;
		
END