/*
drop procedure [dbo].FA_GetDims
Go
-- =============================================
-- Test: exec [dwh].[FA_GetDims] 'TD Investments', 2, 1, 0
-- Test: exec [dwh].[FA_GetDims] 'TD Investments', 2, 1, 1
-- Test: exec [dwh].[FA_GetDims] 'TD Investments', 2, 1, 2
-- Test: exec [dwh].[FA_GetDims] 'TD Investments', 2, 1, 3
-- =============================================
*/
CREATE PROCEDURE [dbo].FA_GetDims
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
		@Offset + [Entry No_] as [Key]
		,min(iif([Dimension Code] = ''COUNTERPARTY'', [Dimension Value Code], NULL)) as [COUNTERPARTY]
		,min(iif([Dimension Code] = ''COSTCENTER'', [Dimension Value Code], NULL)) as [COSTCENTER]
		,min(iif([Dimension Code] = ''DF.EXPENSE'', [Dimension Value Code], NULL)) as [DF.EXPENSE]
		,min(iif([Dimension Code] = ''EXP.OP'', [Dimension Value Code], NULL))  as [EXP.OP]
		,min(iif([Dimension Code] = ''FA.OPERTYPE'', [Dimension Value Code], NULL)) as [FA.OPERTYPE]
		,min(iif([Dimension Code] = ''INC.TAX'', [Dimension Value Code], NULL)) as [INC.TAX]
		,min(iif([Dimension Code] = ''TAX.OTHER'', [Dimension Value Code], NULL))  as [TAX.OTHER]	
		,min(iif([Dimension Code] = ''CURRENCY'', [Dimension Value Code], NULL)) as [CURRENCY]
	from
		[' + @Oltp + '].[dbo].[' + @CompanyName + '$Ledger Entry Dimension] as D with(nolock) 
		inner join ' + @ReadTable +' as S with(nolock)
		on D.[Entry No_] = S.[Key] - @Offset
	where				
		D.[Table ID]= 5601
		and S.[Key] between @StartEntryNo and @EndEntryNo
	group by
		D.[Entry No_]
	order by
		D.[Entry No_]';

	exec(@Script)
	with result sets
	(
		(
			[Key] BIGINT,
			COUNTERPARTY VARCHAR (20),
			COSTCENTER VARCHAR (20),
			[DF.EXPENSE] VARCHAR (20),
			[EXP.OP] VARCHAR (20),
			[FA.OPERTYPE] VARCHAR (20),
			[INC.TAX] VARCHAR (20),
			[TAX.OTHER] VARCHAR (20),
			CURRENCY VARCHAR (20)
		)
	)
end;