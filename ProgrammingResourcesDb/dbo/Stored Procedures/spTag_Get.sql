CREATE PROCEDURE [dbo].[spTag_Get]
	@TagId INT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		[TagId]
		,[Name]
		,[DateCreated] 
	FROM 
		Tag 
	WHERE 
		TagId = @TagId
		
END
