CREATE TABLE [dbo].[CompanyInformation]
(
	[CompanyId] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(100) NULL, 
    [Email] NVARCHAR(244) NULL, 
    [Desciption] NVARCHAR(150) NULL, 
    CONSTRAINT [PK_CompanyInfo] PRIMARY KEY ([CompanyId]) 
)
