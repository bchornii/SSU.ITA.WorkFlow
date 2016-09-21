CREATE TABLE [dbo].[UserAutoReport]
(
	[AutoReportId] INT NOT NULL IDENTITY, 
	[TaskId] INT NOT NULL,	
	[IsActive] BIT NOT NULL,
    [FromTime] TIME NOT NULL, 
    [ToTime] TIME NOT NULL, 
    [RepeatDays] NVARCHAR(100) NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [Comment] NVARCHAR(150) NULL, 
    CONSTRAINT [PK_AutoReport] PRIMARY KEY ([AutoReportId]), 
    CONSTRAINT [FK_AutoReport_ToUserTask] FOREIGN KEY ([TaskId]) REFERENCES [UserTask]([TaskId]) 
	ON DELETE CASCADE
	ON UPDATE CASCADE
)
