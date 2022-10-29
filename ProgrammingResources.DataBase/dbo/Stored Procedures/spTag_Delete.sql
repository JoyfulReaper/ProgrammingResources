CREATE PROCEDURE [dbo].[spTag_Delete]
	@TagId INT
AS
BEGIN
	UPDATE dbo.[Tag]
	SET [DateDeleted] = GETUTCDATE()
	WHERE
		[TagId] = @TagId;
END