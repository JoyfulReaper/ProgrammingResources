CREATE PROCEDURE [dbo].[spResource_GetAll]
AS
BEGIN
	SELECT
		[ResourceId],
		[Title],
		[Url], 
		[Description],
		[ProgrammingLanguageId],
		[TypeId],
		[DateCreated],
		[DateDeleted],
		[UserId]
	FROM
		[Resource]
	WHERE
		[DateDeleted] IS NULL
	ORDER BY
		[DateCreated] DESC;
END