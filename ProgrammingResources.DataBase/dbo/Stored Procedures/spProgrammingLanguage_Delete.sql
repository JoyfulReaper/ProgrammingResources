CREATE PROCEDURE [dbo].[spProgrammingLanguage_Delete]
	@ProgrammingLanguageId INT
AS
BEGIN
	UPDATE dbo.[ProgrammingLanguage]
	SET DateDeleted = GETDATE()
	WHERE
		[ProgrammingLanguageId] = @ProgrammingLanguageId;
END