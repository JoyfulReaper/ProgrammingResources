CREATE PROCEDURE [dbo].[spTag_Get]
	@TagId INT
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
		[TagId] = @TagId;
END