CREATE PROCEDURE [dbo].[SpExample_GetByResource]
	@ResourceId INT
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
	AND [ResourceId] = @ResourceId
	ORDER BY
		[DateCreated] DESC;
END