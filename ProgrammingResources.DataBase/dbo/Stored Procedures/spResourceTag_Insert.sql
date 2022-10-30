CREATE PROCEDURE [dbo].[spResourceTag_Insert]
	@ResourceId INT,
	@TagId INT,
	@UserId NVARCHAR(450)
AS
BEGIN
	INSERT INTO dbo.[ResourceTag]
		(ResourceId,
		TagId,
		UserId)
	SELECT
		@ResourceId,
		@TagId,
		@UserId
	WHERE
		NOT EXISTS(SELECT 1
			FROM [ResourceTag] WITH (UPDLOCK, SERIALIZABLE)
			WHERE ResourceId = @ResourceId
			AND   TagId = @TagId)
END