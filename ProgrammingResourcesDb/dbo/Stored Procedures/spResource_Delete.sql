CREATE PROCEDURE [dbo].[spResource_Delete]
	@ResourceId INT
AS
BEGIN
	UPDATE [Resource]
	SET
		DateDeleted = UTCDATE()
	WHERE
		ResourceId = @ResourceId;
END