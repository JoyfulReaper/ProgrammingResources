CREATE PROCEDURE [dbo].[spProgrammingLanuge_Get]
	@ProgrammingLanguageId INT
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
		ProgrammingLanguageId = @ProgrammingLanguageId;
END
