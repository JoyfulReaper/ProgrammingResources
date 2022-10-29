CREATE PROCEDURE [dbo].[spTag_GetByName]
	@Name NVARCHAR(75)
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
		[Name] = @Name;
END
