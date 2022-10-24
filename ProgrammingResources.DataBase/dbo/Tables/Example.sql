CREATE TABLE [dbo].[Example]
(
	[ExampleId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResourceId] INT NOT NULL, 
    [Text] NVARCHAR(4000) NOT NULL, 
    [Url] NVARCHAR(300) NULL,
    [Page] INT NULL,
    [TypeId] INT NULL,
    [ProgrammingLanguageId] INT NULL,
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [DateDeleted] DATETIME2 NULL, 
    [UserId] NVARCHAR(450) NOT NULL, 
    CONSTRAINT [FK_Example_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [Resource]([ResourceId]), 
    CONSTRAINT [FK_Example_Type] FOREIGN KEY ([TypeId]) REFERENCES [Type]([TypeId]), 
    CONSTRAINT [FK_Example_ProgrammingLanguage] FOREIGN KEY ([ProgrammingLanguageId]) REFERENCES [ProgrammingLanguage]([ProgrammingLanguageId])
)
