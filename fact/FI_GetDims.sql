/*
drop procedure [dbo].FI_GetDims
Go
-- =============================================
-- Test: exec [dwh].[FI_GetDims] 'TD Investments', 2, 30,  0
-- Test: exec [dwh].[FI_GetDims] 'TD Investments', 2, 30,  1
-- Test: exec [dwh].[FI_GetDims] 'TD Investments', 2, 30,  2
-- Test: exec [dwh].[FI_GetDims] 'TD Investments', 2, 30,  3
-- Test: exec [dwh].[FI_GetDims] 'TD Investments', 2, 30,  4
-- =============================================
*/
CREATE PROCEDURE [dbo].FI_GetDims
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
		'[dbo].[FactFI_' + @CompanyName + '_' +  convert(varchar(3), @Partition) + ']' 		

	--set @ReadTable  = '##FactFI_' +  convert(varchar(3), @Partition) 

	set @Script = 
	'
	declare @Offset bigint = 10000000000 * ' + convert(varchar(10), @Chunk) + '
	declare @StartEntryNo bigint = @Offset 
	declare @EndEntryNo   bigint = @Offset + 2147483647

	select
		@Offset + [Entry No_] as [Key]
		,min(iif([Dimension Code] = ''ACCRUAL.TYPE'', [Dimension Value Code], NULL)) as [ACCRUAL.TYPE]
		,min(iif([Dimension Code] = ''BANK.ACC'', [Dimension Value Code], NULL)) as [BANK.ACC]
		,min(iif([Dimension Code] = ''BOOK'', [Dimension Value Code], NULL)) as [BOOK]
		,min(iif([Dimension Code] = ''COMMISSIONS'', [Dimension Value Code], NULL)) as [COMMISSIONS]
		,min(iif([Dimension Code] = ''COSTCENTER'', [Dimension Value Code], NULL)) as [COSTCENTER]
		,min(iif([Dimension Code] = ''COUNTERPARTY'', [Dimension Value Code], NULL)) as [COUNTERPARTY]
		,min(iif([Dimension Code] = ''CURRENCY'', [Dimension Value Code], NULL)) as [CURRENCY]
		,min(iif([Dimension Code] = ''IC'', [Dimension Value Code], NULL))  as [IC]
		,min(iif([Dimension Code] = ''INC.TAX'', [Dimension Value Code], NULL)) as [INC.TAX]
		,min(iif([Dimension Code] = ''AC.COMP'', [Dimension Value Code], NULL)) as [AC.COMP]
		,min(iif([Dimension Code] = ''PROJECT'', [Dimension Value Code], NULL)) as [PROJECT]
		,min(iif([Dimension Code] = ''PROVISIONS'', [Dimension Value Code], NULL)) as [PROVISIONS]
		,min(iif([Dimension Code] = ''COUNTERPARTY.REAL'', [Dimension Value Code], NULL)) as [COUNTERPARTY.REAL]
	from
		[' + @Oltp + '].[dbo].[' + @CompanyName + '$Ledger Entry Dimension] as D with(nolock) 
		inner join ' + @ReadTable +' as S with(nolock)
		on D.[Entry No_] = S.[Key] - @Offset
	where				
		D.[Table ID] = 50220 --or 
		and S.[Key] between @StartEntryNo and @EndEntryNo
	group by
		D.[Entry No_]
	order by
		D.[Entry No_]';
	--print(@Script)
	exec(@Script)
	with result sets
	(
		(
			[Key] BIGINT,
			[ACCRUAL.TYPE] VARCHAR (20),
			[BANK.ACC] VARCHAR (20),
			BOOK VARCHAR (20),
			COMMISSIONS VARCHAR (20),
			COSTCENTER VARCHAR (20),
			COUNTERPARTY VARCHAR (20),
			CURRENCY VARCHAR (20),
			IC VARCHAR (20),
			[INC.TAX] VARCHAR (20),
			[AC.COMP] VARCHAR (20),
			PROJECT VARCHAR (20),
			PROVISIONS VARCHAR (20),
			[COUNTERPARTY.REAL] VARCHAR (20)
		)
	)
end


