CREATE PROCEDURE [dbo].[spResource_GetAll]
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
		[dbo].[Resource]
	WHERE
		DateDeleted IS NULL;
END