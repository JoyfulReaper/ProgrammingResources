CREATE PROCEDURE [dbo].[spType_Insert]
	@Name NVARCHAR(100),
	@UserId NVARCHAR(450)
AS
BEGIN
	INSERT INTO dbo.[Type]
		([Name], [UserId])
	VALUES
		(@Name, @UserId);

	SELECT
		[TypeId], 
		[Name],
		[DateAdded], 
		[DateDeleted], 
		[UserId]
	FROM
		[Type]
	WHERE
		TypeId = SCOPE_IDENTITY();
END
