CREATE TABLE [dbo].[RegistrationToken]
(
	[RegistrationTokenId] INT NOT NULL IDENTITY, 
    [Token] NVARCHAR(32) NOT NULL,
	[UserId] INT NOT NULL, 
    CONSTRAINT [PK_RegistrationToken] PRIMARY KEY ([RegistrationTokenId]),
	CONSTRAINT [FK_RegistrationToken_ToUserInformation] FOREIGN KEY ([UserId]) REFERENCES [UserInformation]([UserId])
)
