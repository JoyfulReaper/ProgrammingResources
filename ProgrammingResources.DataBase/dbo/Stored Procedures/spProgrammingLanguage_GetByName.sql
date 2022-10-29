CREATE PROCEDURE [dbo].[spProgrammingLanguage_GetByName]
	@Language NVARCHAR(100)
AS
BEGIN
	SELECT
		[ProgrammingLanguageId], 
		[Language],
		[DateAdded], 
		[DateDeleted],
		[UserId]
	FROM
		dbo.[ProgrammingLanguage]
	WHERE
		[Language] = @Language;
END