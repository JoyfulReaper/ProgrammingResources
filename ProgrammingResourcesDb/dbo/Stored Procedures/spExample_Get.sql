CREATE PROCEDURE [dbo].[spExample_Get]
	@ExampleId INT
AS
BEGIN
	SELECT
		[ExampleId]
		,[ResourceId]
		,[ExampleText]
		,[DateCreated]
		,[DateDeleted]	
	FROM
		dbo.[Example]
	WHERE
		ExampleId = @ExampleId
	AND DateDeleted IS NULL;
END