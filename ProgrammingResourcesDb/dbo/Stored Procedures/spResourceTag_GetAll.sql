CREATE PROCEDURE [dbo].[spResourceTag_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		[ResourceTagId]
		,[ResourceId]
		,[TagId]
	FROM
		ResourceTag
END