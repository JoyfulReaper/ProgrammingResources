CREATE PROCEDURE [dbo].[spExample_Upsert]
	@ExampleId INT,
	@ResourceId INT,
	@Text NVARCHAR(MAX),
	@Url NVARCHAR(300),
	@Page INT,
	@TypeId INT,
	@ProgrammingLanguageId INT,
	@UserId NVARCHAR(450)
AS
BEGIN
	BEGIN TRANSACTION

	INSERT INTO dbo.[Example]
		([ResourceId],
		[Text],
		[Url],
		[Page],
		[TypeId],
		[ProgrammingLanguageId],
		[UserId])
	SELECT
		@ResourceId,
		@Text,
		@Url,
		@Page,
		@TypeId,
		@ProgrammingLanguageId,
		@UserId
	WHERE NOT EXISTS
	(
		SELECT 1 FROM dbo.[Example] WITH (UPDLOCK, SERIALIZABLE)
		WHERE [ExampleId] = @ExampleId
	);

	IF @@ROWCOUNT = 0
	BEGIN
		UPDATE dbo.[Example]
		SET [Text] = @Text,
			[Url] = @Url,
			[Page] = @Page,
			[TypeId] = @TypeId,
			[ProgrammingLanguageId] = @ProgrammingLanguageId,
			[UserId] = @UserId
		WHERE
			ExampleId = @ExampleId;
	END

	SELECT
		[ExampleId],
		[ResourceId],
		[Text],
		[Url], 
		[Page],
		[TypeId],
		[ProgrammingLanguageId],
		[DateCreated],
		[DateDeleted],
		[UserId]
	FROM
		[Example]
	WHERE
		[ExampleId] = @ExampleId;

	COMMIT TRANSACTION
END