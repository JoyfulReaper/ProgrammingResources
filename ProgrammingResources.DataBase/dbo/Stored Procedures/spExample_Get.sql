CREATE PROCEDURE [dbo].[spExample_Get]
	@ExampleId INT
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
		[ExampleId] = @ExampleId;
END