CREATE PROCEDURE [dbo].[spResource_Delete]
	@ResourceId INT
AS
BEGIN
	UPDATE [Resource]
	SET
		DateDeleted = GETUTCDATE()
	WHERE
		ResourceId = @ResourceId;
END