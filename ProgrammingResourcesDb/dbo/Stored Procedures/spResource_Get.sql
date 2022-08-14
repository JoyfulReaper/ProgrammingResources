CREATE PROCEDURE [dbo].[spResource_Get]
	@ResourceId INT
AS
BEGIN
	SELECT
		[ResourceId]
		,[Title]
		,[Url]
		,[Description]
		,[DateCreated]
	FROM
		[Resource]
	WHERE
		ResourceId = @ResourceId
END