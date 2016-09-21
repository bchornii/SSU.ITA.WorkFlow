CREATE TABLE [dbo].[UserReport]
(
	[ReportId] INT NOT NULL IDENTITY , 
    [TaskId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [CreateDate] DATE NOT NULL, 
    [StartTime] TIME NOT NULL, 
    [EndTime] TIME NOT NULL, 
    [Comment] NVARCHAR(150) NULL, 
    CONSTRAINT [PK_Report] PRIMARY KEY ([ReportId]), 
    CONSTRAINT [FK_Report_ToUserTask] FOREIGN KEY ([TaskId]) REFERENCES [UserTask]([TaskId]), 
    CONSTRAINT [FK_Report_ToUserInformation] FOREIGN KEY ([UserId]) REFERENCES [UserInformation]([UserId])
	ON DELETE CASCADE
	ON UPDATE CASCADE
)
