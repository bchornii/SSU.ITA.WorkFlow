CREATE TABLE [dbo].[UserProjectRelation]
(
	[Id] INT NOT NULL IDENTITY,
    [UserId] INT NOT NULL, 
    [ProjectId] INT NOT NULL, 
    CONSTRAINT [PK_UserProjectRelation] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_UserProjectRelation_ToUserInfo] FOREIGN KEY ([UserId]) REFERENCES [UserInformation]([UserId])
	ON DELETE CASCADE
	ON UPDATE CASCADE, 
    CONSTRAINT [FK_UserProjectRelation_ToUserProject] FOREIGN KEY ([ProjectId]) REFERENCES [UserProject]([ProjectId]) 
	ON DELETE CASCADE
	ON UPDATE CASCADE
)