CREATE PROCEDURE [dbo].[spType_Delete]
	@TypeId INT
AS
BEGIN
	UPDATE dbo.[Type]
	SET DateDeleted = GETUTCDATE()
	WHERE
		TypeId = @TypeId;
END