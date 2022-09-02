CREATE PROCEDURE [dbo].[spExample_Get]
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
		ResourceId = @ResourceId;
END