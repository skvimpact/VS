/*
drop procedure [dbo].FA_GetEntries
Go
-- =============================================
-- Test: exec [dwh].[FA_GetEntries] 'TD Investments', 2, 1, 0
*/
-- =============================================

CREATE PROCEDURE [dbo].FA_GetEntries
(
	@CompanyName	varchar(30),
	@Company		int,
	@Partition		int,
	@Chunk			int,
	@Oltp		varchar(30)
)
as begin
	set nocount on;
	declare @Script varchar(max)
	declare @ReadTable varchar(50) = 
		'[dbo].[FactFA_' + @CompanyName + '_' +  convert(varchar(3), @Partition) + ']' 

	--set @ReadTable  = '##FactFA_' +  convert(varchar(3), @Partition) 

	set @Script = 
	'
	declare @Offset bigint = 10000000000 * ' + convert(varchar(10), @Chunk) + '
	declare @StartEntryNo bigint = @Offset 
	declare @EndEntryNo   bigint = @Offset + 2147483647

	select
		S.[Key],
		d.[Entry No_] , 
		d.[FA no_],
		d.[Posting Date],
		d.[Document No_],
		d.[Description],
		d.[Depreciation Book Code],
		CASE d.[FA Posting Type]
			WHEN 0 THEN ''Acqusition cost''
			WHEN 1 THEN ''Depreciation''
			WHEN 2 THEN ''Write-Down''
			WHEN 3 THEN ''Appreciation''
			WHEN 4 THEN ''Custom 1''
			WHEN 5 THEN ''Custom 2''
			WHEN 6 THEN ''Proceeds on Disposal''
			WHEN 7 THEN ''Salvage Value''
			WHEN 8 THEN ''Gain/Loss''
			WHEN 9 THEN ''Book value on Disposal''
			WHEN 10 THEN ''Transfer''
		END AS [FA Posting Type],
		d.[FA Posting Group],
		d.[User ID],
		d.[No_ of Depreciation Days],
		cast(D.[Amount] as decimal(28,5)) as [Amount], 
		cast(D.[Quantity] as decimal(28,5)) as [Quantity], 
		cast(hashbytes(''md5'', d.[Document No_]) as bigint) as [SK_Document], --NULL, --Doc.id,
		cast(hashbytes(''md5'', nullif(LTRIM(RTRIM(REPLACE(REPLACE(d.Description, CHAR(10), ''''), CHAR(13), ''''))),'''')) as bigint) as [SK_Description] --NULL --Descr.id

	from
		[' + @Oltp + '].[dbo].[' + @CompanyName + '$FA Ledger Entry] as d	WITH(nolock) 
		
		inner join ' + @ReadTable +' as S WITH(nolock) 
		on d.[Entry No_] = S.[Key] - @Offset
	where	
		S.[Key] between @StartEntryNo and @EndEntryNo
	order by S.[Key]';


	--print(@Script)
	
	exec(@Script)
	with result sets
	(
		(
			[Key] BIGINT,
			[Entry No_] INT,
			[FA no_] VARCHAR (20),
			[Posting Date] DATETIME,
			[Document No_] VARCHAR (20),
			[Description] VARCHAR (50),
			[Depreciation Book Code] VARCHAR (10),
			[FA Posting Type] VARCHAR (22),
			[FA Posting Group] VARCHAR (10),
			[User ID] VARCHAR (20),
			[No_ of Depreciation Days] INT,
			Amount DECIMAL (28, 5),
			Quantity DECIMAL (28, 5),
			SK_Document BIGINT,
			SK_Description BIGINT
		)
	)
end;