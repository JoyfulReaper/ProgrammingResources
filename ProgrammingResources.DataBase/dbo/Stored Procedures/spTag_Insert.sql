CREATE PROCEDURE [dbo].[spTag_Insert]
	@Name NVARCHAR(75),
	@UserId NVARCHAR(450)
AS
BEGIN
	INSERT INTO dbo.[Tag]
		([Name], [UserId])
	VALUES
		(@Name, @UserId);


	SELECT
		[TagId], [Name], [DateCreated], [DateDeleted], [UserId]
	FROM
		Tag
	WHERE
		TagId = SCOPE_IDENTITY();
END