CREATE TABLE [dbo].[Example]
(
	[ExampleId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResourceId] INT NOT NULL, 
    [ExampleText] NVARCHAR(4000) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateDeleted] DATETIME2 NULL, 
    CONSTRAINT [FK_Example_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [Resource]([ResourceId])
)
