CREATE TABLE [dbo].[Tag]
(
	[TagId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) UNIQUE NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
)
