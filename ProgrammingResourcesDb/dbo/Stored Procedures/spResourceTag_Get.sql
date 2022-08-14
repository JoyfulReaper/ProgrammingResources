CREATE PROCEDURE [dbo].[spResourceTag_Get]
	@ResourceTagId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		[ResourceTagId]
		,[ResourceId]
		,[TagId]
	FROM
		ResourceTag
	WHERE
		ResourceTagId = @ResourceTagId;
END