CREATE PROCEDURE [dbo].[spType_Delete]
	@TypeId INT
AS
BEGIN
	UPDATE dbo.[Type]
	SET DateDeleted = GETDATE()
	WHERE
		TypeId = @TypeId;
END