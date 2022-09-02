CREATE TABLE [dbo].[Resource]
(
	[ResourceId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Url] NVARCHAR(500) NOT NULL, 
    [Description] NVARCHAR(1000) NULL, 
    [ProgrammingLanguage] NVARCHAR(100) NULL,
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateDeleted] DATETIME2 NULL
)
