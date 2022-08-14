﻿CREATE PROCEDURE [dbo].[spTag_Save]
	@TagId INT,
	@Name NVARCHAR(50)
AS
BEGIN

BEGIN TRANSACTION;
 
	INSERT dbo.Tag
		([Name])
	  SELECT
		@Name
	  WHERE NOT EXISTS
	  (
		SELECT 1 FROM dbo.Tag WITH (UPDLOCK, SERIALIZABLE)
		  WHERE 
			[TagId] = @TagId
	  );
 
	IF @@ROWCOUNT = 0
	BEGIN
	  UPDATE dbo.Tag
		SET
			[Name] = @Name
		WHERE
			[TagId] = @TagId;
			
		SET @TagId = SCOPE_IDENTITY();
	END
 
COMMIT TRANSACTION;

SELECT @TagId

END