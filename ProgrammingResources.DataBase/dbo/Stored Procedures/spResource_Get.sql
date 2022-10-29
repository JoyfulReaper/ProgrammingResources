CREATE PROCEDURE [dbo].[spResource_Get]
	@ResourceId INT
AS
BEGIN
	SELECT
		[ResourceId],
		[Title],
		[Url],
		[Description],
		[ProgrammingLanguageId],
		[TypeId], 
		[DateCreated],
		[DateDeleted], 
		[UserId]
	FROM
		[Resource]
	WHERE
		ResourceId = @ResourceId
END
