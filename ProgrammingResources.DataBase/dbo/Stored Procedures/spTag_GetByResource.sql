CREATE PROCEDURE [dbo].[spTag_GetByResource]
	@ResourceId INT
AS
BEGIN
	SELECT
		t.[TagId],
		t.[Name],
		t.[DateCreated],
		t.[DateDeleted],
		t.[UserId]
	FROM
		dbo.[Tag] t INNER JOIN
		dbo.[ResourceTag] rt ON t.TagId = rt.TagId
	WHERE
		rt.[ResourceId] = @ResourceId
	ORDER BY
		[Name]
	
END
