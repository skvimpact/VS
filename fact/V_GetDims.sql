/*
drop procedure [dbo].V_GetDims
>>>>>>>>>> Test >>>>>>>>>>
exec [dwh].[V_GetDims] 'TD Investments', 2, 1, 0
exec [dwh].[V_GetDims] 'TD Investments', 2, 1, 1
<<<<<<<<<< Test <<<<<<<<<<
*/
CREATE PROCEDURE [dbo].V_GetDims
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
		'[dbo].[FactV_' + @CompanyName + '_' +  convert(varchar(3), @Partition) + ']' 		

	--set @ReadTable  = '##FactV_' +  convert(varchar(3), @Partition) 

	set @Script = 
	'
	declare @Offset bigint = 10000000000 * ' + convert(varchar(10), @Chunk) + '
	declare @StartEntryNo bigint = @Offset 
	declare @EndEntryNo   bigint = @Offset + 2147483647

		select
			@Offset + [Entry No_] as [Key]
			,min(iif([Dimension Code] = ''BANK.ACC'', [Dimension Value Code], NULL)) as [BANK.ACC]
			,min(iif([Dimension Code] = ''BOOK'', [Dimension Value Code], NULL)) as [BOOK]
			,min(iif([Dimension Code] = ''COSTCENTER'', [Dimension Value Code], NULL)) as [COSTCENTER]
			,min(iif([Dimension Code] = ''COUNTERPARTY'', [Dimension Value Code], NULL)) as [COUNTERPARTY]
			,min(iif([Dimension Code] = ''COUNTERPARTY.REAL'', [Dimension Value Code], NULL)) as [COUNTERPARTY.REAL]
			,min(iif([Dimension Code] = ''CURRENCY'', [Dimension Value Code], NULL)) as [CURRENCY]
			,min(iif([Dimension Code] = ''DEAL'', [Dimension Value Code], NULL))  as [DEAL]
			,min(iif([Dimension Code] = ''IC'', [Dimension Value Code], NULL))  as [IC]
			,min(iif([Dimension Code] = ''FA'', [Dimension Value Code], NULL))  as [FA]
			,min(iif([Dimension Code] = ''COMMISSIONS'', [Dimension Value Code], NULL)) as [COMMISSIONS]
			,min(iif([Dimension Code] = ''EXP.OP'', [Dimension Value Code], NULL))  as [EXP.OP]
			,min(iif([Dimension Code] = ''PROVISIONS'', [Dimension Value Code], NULL)) as [PROVISIONS]
			,min(iif([Dimension Code] = ''PROV.MOVEMENTS'', [Dimension Value Code], NULL))  as [PROV.MOVEMENTS]
			,min(iif([Dimension Code] = ''RECONC.TYPE'', [Dimension Value Code], NULL)) as [RECONC.TYPE]
			,min(iif([Dimension Code] = ''TAX.JUR'', [Dimension Value Code], NULL))  as [TAX.JUR]
			,min(iif([Dimension Code] = ''TAX.OTHER'', [Dimension Value Code], NULL))  as [TAX.OTHER]
			,min(iif([Dimension Code] = ''INC.TAX'', [Dimension Value Code], NULL)) as [INC.TAX]
			,min(iif([Dimension Code] = ''AC.COMP'', [Dimension Value Code], NULL)) as [AC.COMP]
			,min(iif([Dimension Code] = ''REVAL.TYPE'', [Dimension Value Code], NULL)) as [REVAL.TYPE]
			,min(iif([Dimension Code] = ''ACCRUAL.TYPE'', [Dimension Value Code], NULL)) as [ACCRUAL.TYPE]
			,min(iif([Dimension Code] = ''PROJECT'', [Dimension Value Code], NULL)) as [PROJECT]
			,min(iif([Dimension Code] = ''GW.OPERTYPE'', [Dimension Value Code], NULL)) as [GW.OPERTYPE]
			,min(iif([Dimension Code] = ''FA.OPERTYPE'', [Dimension Value Code], NULL)) as [FA.OPERTYPE]
			,min(iif([Dimension Code] = ''DF.EXPENSE'', [Dimension Value Code], NULL)) as [DF.EXPENSE]
			,min(iif([Dimension Code] = ''FUND.STATUS'', [Dimension Value Code], NULL)) as [FUND.STATUS]
			,min(iif([Dimension Code] = ''FIN.INSTR'', [Dimension Value Code], NULL)) as [FIN.INSTR]
			,min(iif([Dimension Code] = ''CAPITAL.MOVE'', [Dimension Value Code], NULL)) as [CAPITAL.MOVE]
		from
			[' + @Oltp + '].[dbo].[' + @CompanyName + '$Ledger Entry Dimension] as D with(nolock) 
			inner join ' + @ReadTable +' as S with(nolock)
			on D.[Entry No_] = S.[Key] - @Offset
		where				
			D.[Table ID]= 17
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
			[BANK.ACC] VARCHAR (20),
			BOOK VARCHAR (20),
			--DESK VARCHAR (20),
			COSTCENTER VARCHAR (20),
			COUNTERPARTY VARCHAR (20),
			[COUNTERPARTY.REAL] VARCHAR (20),
			CURRENCY VARCHAR (20),
			DEAL VARCHAR (20),
			IC VARCHAR (20),
			FA VARCHAR (20),
			COMMISSIONS VARCHAR (20),
			[EXP.OP] VARCHAR (20),
			PROVISIONS VARCHAR (20),
			[PROV.MOVEMENTS] VARCHAR (20),
			[RECONC.TYPE] VARCHAR (20),
			[TAX.JUR] VARCHAR (20),
			[TAX.OTHER] VARCHAR (20),
			[INC.TAX] VARCHAR (20),
			[AC.COMP] VARCHAR (20),
			[REVAL.TYPE] VARCHAR (20),
			[ACCRUAL.TYPE] VARCHAR (20),
			PROJECT VARCHAR (20),
			[GW.OPERTYPE] VARCHAR (20),
			[FA.OPERTYPE] VARCHAR (20),
			[DF.EXPENSE] VARCHAR (20),
			[FUND.STATUS] VARCHAR (20),
			[FIN.INSTR] VARCHAR (20),
			[CAPITAL.MOVE] VARCHAR (20)
		)
	)
end;