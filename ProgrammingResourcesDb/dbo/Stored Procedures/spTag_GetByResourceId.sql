CREATE PROCEDURE [dbo].[spTag_GetByResourceId]
	@ResourceId INT
AS
BEGIN
	SELECT
		[t].[TagId]
		,[t].[Name]
		,[t].[DateCreated]
	FROM
		[ResourceTag] rt INNER JOIN
		[Tag] t on t.[TagId] = rt.TagId
	WHERE
		[ResourceId] = @ResourceId;
END