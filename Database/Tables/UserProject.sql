CREATE TABLE [dbo].[UserProject]
(
	[ProjectId] INT NOT NULL IDENTITY , 
	[StatusId] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [CreatorId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [Description] NVARCHAR(150) NULL, 
    CONSTRAINT [PK_UserProject] PRIMARY KEY ([ProjectId]),
	CONSTRAINT [FK_Project_ToProjectStatus] FOREIGN KEY ([StatusId]) REFERENCES [ProjectStatus]([StatusId])	
)
