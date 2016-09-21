CREATE TABLE [dbo].[SessionToken]
(
	[SessionTokenId] INT NOT NULL IDENTITY, 
    [Token] NVARCHAR(32) NOT NULL, 
    [ExpirationDate] DATETIME NOT NULL,
	[UserId] INT NOT NULL, 
    CONSTRAINT [PK_SessionToken] PRIMARY KEY ([SessionTokenId]),
	CONSTRAINT [FK_SessionToken_ToUserInformation] FOREIGN KEY ([UserId]) REFERENCES [UserInformation]([UserId])
)
