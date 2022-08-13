CREATE TABLE [dbo].[ResourceTag]
(
	[ResourceTagId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResourceId] INT NOT NULL, 
    [TagId] INT NOT NULL, 
    CONSTRAINT [FK_ResourceTag_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [Resource]([ResourceId]),
    CONSTRAINT [FK_ResourceTag_Tag] FOREIGN KEY ([TagId]) REFERENCES [Tag]([TagId])
)
