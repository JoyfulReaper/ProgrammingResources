CREATE TABLE [dbo].[ResourceTag]
(
	[ResourceTagId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResourceId] INT NOT NULL, 
    [TagId] INT NOT NULL, 
    [DateAdded] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateDeleted] DATETIME2 NULL, 
    [UserId] NVARCHAR(450) NOT NULL
)
