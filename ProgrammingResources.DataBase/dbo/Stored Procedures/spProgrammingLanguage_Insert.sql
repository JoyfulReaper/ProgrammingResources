CREATE PROCEDURE [dbo].[spProgrammingLanguage_Insert]
	@Language NVARCHAR(100),
	@UserId NVARCHAR(450)
AS
BEGIN
	INSERT INTO dbo.[ProgrammingLanguage]
		([Language],
		[UserId])
	VALUES
		(@Language,
		@UserId);


	SELECT
		[ProgrammingLanguageId], 
		[Language],
		[DateAdded], 
		[DateDeleted],
		[UserId]
	FROM
		ProgrammingLanguage
	WHERE
		ProgrammingLanguageId = SCOPE_IDENTITY();
END