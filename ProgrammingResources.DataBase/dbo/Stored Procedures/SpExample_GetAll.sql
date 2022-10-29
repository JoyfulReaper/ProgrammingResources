CREATE PROCEDURE [dbo].[SpExample_GetAll]
AS
BEGIN
	SELECT
		[ExampleId],
		[ResourceId], 
		[Text], 
		[Url], 
		[Page], 
		[TypeId], 
		[ProgrammingLanguageId],
		[DateCreated], 
		[DateDeleted], 
		[UserId]
	FROM
		dbo.[Example]
	WHERE
		[DateDeleted] IS NULL
	ORDER BY
		ResourceId;
END