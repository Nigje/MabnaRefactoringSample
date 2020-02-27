IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))

BEGIN

Drop TABLE [dbo].[Users] 

END
Go
CREATE TABLE [dbo].[Users] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [UserName] VARCHAR (50) NOT NULL,
    [Email]    VARCHAR (50) NOT NULL
);
Go