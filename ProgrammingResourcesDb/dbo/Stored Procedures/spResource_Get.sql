CREATE PROCEDURE [dbo].[spResource_Get]
	@ResourceId INT
AS
BEGIN
	SELECT
		[ResourceId]
		,[Title]
		,[Url]
		,[Description]
		,[ProgrammingLanguage]
		,[DateCreated]
	FROM
		[Resource]
	WHERE
		ResourceId = @ResourceId
	AND DateDeleted IS NULL;
END