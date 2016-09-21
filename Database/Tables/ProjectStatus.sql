CREATE TABLE [dbo].[ProjectStatus]
(
	[StatusId] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_ProjectStatus] PRIMARY KEY ([StatusId])
)
