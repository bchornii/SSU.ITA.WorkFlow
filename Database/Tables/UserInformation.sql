CREATE TABLE [dbo].[UserInformation]
(
	[UserId] INT NOT NULL IDENTITY , 
    [RoleId] INT NOT NULL, 
    [CompanyId] INT NOT NULL, 
    [ManagerId] INT NOT NULL, 
    [Email] NVARCHAR(244) NOT NULL, 
    [Password] NVARCHAR(256) NOT NULL, 
    [Salt] NVARCHAR(MAX) NOT NULL, 
    [Name] NVARCHAR(50) NULL, 
    [SurName] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(100) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [Photo] NVARCHAR(100) NULL,
    [IsConfirmed] BIT NOT NULL, 
    CONSTRAINT [PK_UserInformation] PRIMARY KEY ([UserId]), 
    CONSTRAINT [FK_UserInformation_ToUserRole] FOREIGN KEY ([RoleId]) REFERENCES [UserRole]([RoleId]), 
    CONSTRAINT [FK_UserInformation_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [CompanyInformation]([CompanyId])
	ON DELETE CASCADE
	ON UPDATE CASCADE
)
