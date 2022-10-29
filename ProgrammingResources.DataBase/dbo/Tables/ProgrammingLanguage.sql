CREATE TABLE [dbo].[ProgrammingLanguage]
(
	[ProgrammingLanguageId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Language] NVARCHAR(100) NOT NULL, 
    [DateAdded] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateDeleted] DATETIME2 NULL, 
    [UserId] NVARCHAR(450) NOT NULL
)
