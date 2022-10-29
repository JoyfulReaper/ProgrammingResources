CREATE PROCEDURE [dbo].[spTag_GetAll]
AS
BEGIN
	SELECT
		[TagId],
		[Name],
		[DateCreated],
		[DateDeleted],
		[UserId]
	FROM
		dbo.[Tag]
	WHERE
		DateDeleted IS NULL
	ORDER BY
		[Name];
END
