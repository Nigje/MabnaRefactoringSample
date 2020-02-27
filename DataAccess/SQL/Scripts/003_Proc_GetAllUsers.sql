IF ( OBJECT_ID('GetAllUsers') IS NOT NULL )
    BEGIN
        DROP PROCEDURE GetAllUsers
    END
GO
CREATE PROCEDURE [dbo].GetAllUsers
	
AS
	SELECT * from Users
	Go