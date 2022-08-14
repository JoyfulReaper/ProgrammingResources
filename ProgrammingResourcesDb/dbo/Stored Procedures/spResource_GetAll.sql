CREATE PROCEDURE [dbo].[spResource_GetAll]
AS
BEGIN
	SELECT 
		[ResourceId]
		,[Title]
		,[Url]
		,[Description]
		,[DateCreated]	
	FROM
		[dbo].[Resource]
END