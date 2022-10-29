CREATE PROCEDURE [dbo].[spResourceTag_Delete]
	@ResourceTagId INT,
	@TagId INT
AS
BEGIN
	UPDATE dbo.[ResourceTag]
	SET [DateDeleted] = GETUTCDATE()
	WHERE 
		ResourceTagId = @ResourceTagId
	AND TagId = @TagId;

END