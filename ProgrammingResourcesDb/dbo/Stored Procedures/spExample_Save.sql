CREATE PROCEDURE [dbo].[spExample_Save]
	@ExampleId INT,
	@ResourceId INT,
	@Example NVARCHAR(4000)
AS
BEGIN
BEGIN TRANSACTION

	INSERT dbo.[Example]
		(ResourceId,
		ExampleText)
	SELECT
		@ResourceId,
		@Example
	WHERE NOT EXISTS
	(
		SELECT 1 FROM dbo.[Example] WITH (UPDLOCK, SERIALIZABLE)
		WHERE
			[ExampleId] = @ExampleId
	);

	IF @@ROWCOUNT = 0
	BEGIN
		UPDATE dbo.[Example]
			SET [ExampleText] = @Example
		WHERE
			ExampleId = @ExampleId
	END
	ELSE
		BEGIN
			SET @ExampleId = SCOPE_IDENTITY();
		END

COMMIT TRANSACTION

SELECT @ExampleId;

END