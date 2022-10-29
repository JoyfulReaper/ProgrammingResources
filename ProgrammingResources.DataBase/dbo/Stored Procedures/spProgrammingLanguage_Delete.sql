CREATE PROCEDURE [dbo].[spProgrammingLanguage_Delete]
	@ProgrammingLanguageId INT
AS
BEGIN
	UPDATE dbo.[ProgrammingLanguage]
	SET DateDeleted = GETUTCDATE()
	WHERE
		[ProgrammingLanguageId] = @ProgrammingLanguageId;
END