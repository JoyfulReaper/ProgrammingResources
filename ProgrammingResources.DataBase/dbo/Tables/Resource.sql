CREATE TABLE [dbo].[Resource]
(
	[ResourceId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Url] NVARCHAR(500) NULL, 
    [Description] NVARCHAR(1000) NULL, 
    [ProgrammingLanguageId] INT NULL, 
    [TypeId] INT NULL,
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateDeleted] DATETIME2 NULL, 
    [UserId] NVARCHAR(450) NOT NULL, 
    CONSTRAINT [FK_Resource_ProgrammingLanguage] FOREIGN KEY ([ProgrammingLanguageId]) REFERENCES [ProgrammingLanguage]([ProgrammingLanguageId]), 
    CONSTRAINT [FK_Resource_Type] FOREIGN KEY ([TypeId]) REFERENCES [Type]([TypeId])
)
