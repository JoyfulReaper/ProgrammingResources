CREATE PROCEDURE [dbo].[spResourceTag_Insert]
	@ResourceId INT,
	@TagId INT,
	@UserId NVARCHAR(450)
AS
BEGIN
	INSERT INTO dbo.[ResourceTag]
		(ResourceId, TagId, UserId)
	VALUES
		(@ResourceId, @TagId, @UserId);
END