CREATE PROCEDURE [dbo].[spResource_Save]
	@ResourceId INT,
	@Title NVARCHAR(100),
	@Url NVARCHAR(500),
	@Description NVARCHAR(1000),
	@ProgrammingLanguage NVARCHAR(100)
AS
BEGIN

BEGIN TRANSACTION;
 
	INSERT dbo.[Resource]
		([Title],
		[Url],
		[Description],
		[ProgrammingLanguage])
	  SELECT
		@Title,
		@Url,
		@Description,
		@ProgrammingLanguage
	  WHERE NOT EXISTS
	  (
		SELECT 1 FROM dbo.[Resource] WITH (UPDLOCK, SERIALIZABLE)
		  WHERE 
			[ResourceId] = @ResourceId
	  );
 
	IF @@ROWCOUNT = 0
		BEGIN
		  UPDATE dbo.[Resource]
			SET
				[Title] = @Title,
				[Url] = @Url,
				[Description] = @Description,
				[ProgrammingLanguage] = @ProgrammingLanguage
			WHERE
				[ResourceId] = @ResourceId;
		END
		ELSE
			BEGIN
				SET @ResourceId = SCOPE_IDENTITY();
			END
 
COMMIT TRANSACTION;

SELECT @ResourceId

END