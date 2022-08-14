CREATE PROCEDURE [dbo].[spTag_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		[TagId]
		,[Name]
		,[DateCreated]
	FROM
		Tag;
END