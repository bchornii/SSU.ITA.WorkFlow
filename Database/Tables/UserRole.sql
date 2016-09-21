CREATE TABLE [dbo].[UserRole]
(
	[RoleId] INT NOT NULL  IDENTITY, 
    [Name] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([RoleId])
)
