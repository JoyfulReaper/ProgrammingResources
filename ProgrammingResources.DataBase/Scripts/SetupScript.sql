-- TODO: Automaticlly Seed DB

-- Add intial Resource / Example Types
INSERT INTO [dbo].[Type]
	([Name], [UserId])
VALUES
	('Website', 'SYSTEM'),
	('Video', 'SYSTEM'),
	('Book', 'SYSTEM');
GO

-- Add inital Programming Languages
INSERT INTO [dbo].[ProgrammingLanguage]
	([Language], [UserId])
VALUES
	('C#', 'SYSTEM'),
	('JavaScript', 'SYSTEM'),
	('Java', 'SYSTEM'),
	('Python', 'SYSTEM'),
	('Rust', 'SYSTEM'),
	('TypeScript', 'SYSTEM')