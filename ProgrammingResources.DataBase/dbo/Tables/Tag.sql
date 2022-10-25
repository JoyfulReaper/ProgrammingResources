﻿CREATE TABLE [dbo].[Tag]
(
	[TagId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(75) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [DateDeleted] DATETIME2 NULL, 
    [UserId] NVARCHAR(450) NOT NULL
)