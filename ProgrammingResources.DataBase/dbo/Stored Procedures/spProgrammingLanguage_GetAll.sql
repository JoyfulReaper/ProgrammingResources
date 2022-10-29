CREATE PROCEDURE [dbo].[spProgrammingLanguage_GetAll]
AS
BEGIN
	SELECT 
		[ProgrammingLanguageId], 
		[Language], 
		[DateAdded], 
		[DateDeleted],
		[UserId]
	FROM
		ProgrammingLanguage
	WHERE
		DateDeleted IS NULL
	ORDER BY
		[Language];
END
