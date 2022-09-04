CREATE PROCEDURE [dbo].[spExample_GetByResource]
	@ResourceId INT
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
		ResourceId = @ResourceId
	AND DateDeleted IS NULL;
END