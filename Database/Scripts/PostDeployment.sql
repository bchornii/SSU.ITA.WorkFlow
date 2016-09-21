
/* Add roles to databases */
if not exists (select * from dbo.UserRole)
	insert into dbo.UserRole
	values
	('Admin'),
	('Manager'),
	('Employee'),		
	('Anonymous')
GO

/* Add companies */
if not exists (select * from dbo.CompanyInformation)
	insert into dbo.CompanyInformation
	(Name,PhoneNumber,[Address],Email,Desciption)
	values
	('Apple','380666667678','USA','apple@apple.com','This is great company'),
	('Microsoft','4090934090304','USA','microsoft@gmail.com','This is great company')
GO

/* Add users */
if not exists (select * from dbo.UserInformation)
	insert into dbo.UserInformation
	(RoleId,CompanyId,ManagerId,Email,[Password],Salt,Name,SurName,[Address],PhoneNumber,Photo,IsConfirmed)
	values
	((select RoleId from dbo.UserRole where Name = 'Manager'),
	 (select CompanyId from dbo.CompanyInformation where Name = 'Apple'),
	 0,
	 'rostyk1@meta.ua',
	 'DBTlUqwTwYDcLWSoZyQsTdhQ04tthwoDrUdW4uHjNzcIOcY1ciooRPLnG+X6UhelR+VzHNB5qkV2NljT+5K3pvrrT2sFYJ8uMJk/S8CHd7Y=',
	 '+utPawVgny4wmT9LwId3tg==',
	 'Rostyslav',
	 'Perozhak',
	 'USA',
	 '0989086678',
	 NULL,
	 1
	)
GO

declare @userId int =(select UserId from dbo.UserInformation where Email = 'rostyk1@meta.ua');

update dbo.UserInformation
set ManagerId = @userId
where UserId = @userId

/* Add project statuses */
if not exists (select * from dbo.ProjectStatus)
 insert into dbo.ProjectStatus
 values
 ('Not active'),
 ('Active'),
 ('Finished'),
 ('Overdued')
GO

/* Add task statuses */
if not exists (select * from dbo.TaskStatus)
 insert into dbo.TaskStatus
 values
 ('Not active'),
 ('Active'),
 ('Finished'),
 ('Overdued')
GO

	


