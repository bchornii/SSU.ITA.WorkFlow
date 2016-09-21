CREATE TABLE [dbo].[TaskStatus]
(
	[StatusId] INT NOT NULL IDENTITY , 
    [Name] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_TaskStatus] PRIMARY KEY ([StatusId])
)
