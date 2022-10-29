CREATE PROCEDURE [dbo].[spType_Get]
	@TypeId INT
AS
BEGIN
	SELECT 
		[TypeId], 
		[Name],
		[DateAdded], 
		[DateDeleted],
		[UserId]
	FROM
		[Type]
	WHERE
		TypeId = @TypeId;
END