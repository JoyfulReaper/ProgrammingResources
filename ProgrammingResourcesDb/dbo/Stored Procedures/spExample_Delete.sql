CREATE PROCEDURE [dbo].[spExample_Delete]
	@ExampleId INT
AS
BEGIN
	UPDATE dbo.[Example]
		SET DateDeleted = GETUTCDATE()
	WHERE
		ExampleId = @ExampleId;
END