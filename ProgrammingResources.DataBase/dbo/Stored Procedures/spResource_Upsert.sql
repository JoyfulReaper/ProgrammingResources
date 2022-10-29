CREATE PROCEDURE [dbo].[spResource_Upsert]
	@ResourceId INT,
	@Title NVARCHAR(100),
	@Url NVARCHAR(500),
	@Description NVARCHAR(1000),
	@ProgrammingLanguageId INT,
	@TypeId INT,
	@UserId NVARCHAR(450)
AS
BEGIN
	BEGIN TRANSACTION

		INSERT dbo.[Resource]
			([Title],
			[Url],
			[Description],
			[ProgrammingLanguageId],
			[TypeId],
			[UserId])
		SELECT
			@Title,
			@Url,
			@Description,
			@ProgrammingLanguageId,
			@TypeId,
			@UserId
		WHERE NOT EXISTS
		(
			SELECT 1 FROM dbo.[Resource] WITH (UPDLOCK, SERIALIZABLE)
			WHERE [ResourceId] = @ResourceId
		);

		IF @@ROWCOUNT = 0
		BEGIN
			UPDATE dbo.[Resource]
			SET [Title] = @Title,
				[Url] = @Url,
				[Description] = @Description,
				[ProgrammingLanguageId] = @ProgrammingLanguageId,
				[TypeId] = @TypeId,
				[UserId] = @UserId
			WHERE
				[ResourceId] = @ResourceId;
		END
		ELSE
			SET @ResourceId = SCOPE_IDENTITY();

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
			[ResourceId] = @ResourceId;

	COMMIT TRANSACTION
END
