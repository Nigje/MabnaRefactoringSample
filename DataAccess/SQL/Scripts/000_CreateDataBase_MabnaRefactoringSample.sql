DECLARE @dbname nvarchar(128)
SET @dbname = N'MabnaRefactoringSample'

IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))
Begin
DROP DATABASE MabnaRefactoringSample
End
Go
CREATE DATABASE MabnaRefactoringSample
Go