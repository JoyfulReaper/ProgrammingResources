﻿CREATE TABLE [dbo].[Type]
(
	[TypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [DateAdded] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [DateDeleted] DATETIME2 NULL, 
    [UserId] NVARCHAR(450) NOT NULL
)
