IF ( OBJECT_ID('GetUserByIdAndUserName') IS NOT NULL )
    BEGIN
        DROP PROCEDURE GetUserByIdAndUserName
    END
GO
CREATE PROCEDURE [dbo].GetUserByIdAndUserName
	@id int ,
	@userName varchar(50)
AS
Select * from Users where Id=@id and Username=@userName
Go