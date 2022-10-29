CREATE PROCEDURE [dbo].[spType_GetAll]
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
		DateDeleted IS NULL
	ORDER BY
		[Name];
END