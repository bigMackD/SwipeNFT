IF NOT EXISTS (SELECT 1 FROM dbo.AspNetRoles WHERE [Name] = 'Admin')
BEGIN
   INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName])
     VALUES
           (1
           ,'Admin'
           ,'ADMIN')
END

IF NOT EXISTS (SELECT 1 FROM dbo.AspNetRoles WHERE [Name] = 'User')
BEGIN
   INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName])
     VALUES
           (2
           ,'User'
           ,'USER')
END
