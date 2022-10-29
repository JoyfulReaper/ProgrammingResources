CREATE PROCEDURE [dbo].[spType_GetByName]
	@Name NVARCHAR(100)
AS
BEGIN
	SELECT
		[TypeId], 
		[Name],
		[DateAdded],
		[DateDeleted], 
		[UserId]
	FROM
		dbo.[Type]
	WHERE
		[Name] = @Name;
END