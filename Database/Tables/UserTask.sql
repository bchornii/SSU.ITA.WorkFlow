CREATE TABLE [dbo].[UserTask]
(
	[TaskId] INT NOT NULL IDENTITY , 
    [Name] NVARCHAR(50) NOT NULL, 
    [ProjectId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [StatusId] INT NOT NULL, 
    [Description] NVARCHAR(150) NULL, 
    CONSTRAINT [PK_UserTask] PRIMARY KEY ([TaskId]), 
    CONSTRAINT [FK_UserTask_ToUserProject] FOREIGN KEY ([ProjectId]) REFERENCES [UserProject]([ProjectId])
	ON DELETE CASCADE
	ON UPDATE CASCADE, 
    CONSTRAINT [FK_Tasks_ToUserInformation] FOREIGN KEY ([UserId]) REFERENCES [UserInformation]([UserId])
	ON DELETE CASCADE
	ON UPDATE CASCADE, 
    CONSTRAINT [FK_Tasks_ToWorkStatus] FOREIGN KEY ([StatusId]) REFERENCES [TaskStatus]([StatusId])		
)
