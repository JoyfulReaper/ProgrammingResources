--https://stackoverflow.com/questions/639854/check-if-a-row-exists-otherwise-insert
--The updlock hint forces the query to take an update lock on the row if it already exists, preventing other transactions from modifying it until you commit or roll back.

--The holdlock hint forces the query to take a range lock, preventing other transactions from adding a row matching your filter criteria until you commit or roll back.

--The rowlock hint forces lock granularity to row level instead of the default page level, so your transaction 
--won't block other transactions trying to update unrelated rows in the same page (but be aware of the trade-off between reduced contention and the increase in locking overhead
--- you should avoid taking large numbers of row-level locks in a single transaction).

CREATE PROCEDURE [dbo].[spResourceTag_Save]
	@ResourceId INT,
	@TagId INT
AS
BEGIN
	BEGIN TRANSACTION
		IF NOT EXISTS (SELECT 1 FROM ResourceTag WITH (UPDLOCK, ROWLOCK, HOLDLOCK) WHERE ResourceId = @ResourceId AND TagId = @TagId)
			INSERT INTO ResourceTag
				(ResourceId,
				TagId)
			SELECT
				@ResourceId,
				@TagId;

	COMMIT TRANSACTION
END