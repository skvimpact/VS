/*
drop procedure [dbo].FI_GetEntries
Go
-- =============================================
exec [dbo].[FI_GetEntries] 'SIB Cyprus', 4, 4, 0, 'Nav-Offshores-Day'
-- Test: exec [dwh].[FI_GetEntries] 'TD Investments', 30, 2, 1
-- Test: exec [dwh].[FI_GetEntries] 'TD Investments', 30, 2, 2
-- Test: exec [dwh].[FI_GetEntries] 'TD Investments', 30, 2, 3
-- Test: exec [dwh].[FI_GetEntries] 'TD Investments', 30, 2, 4
-- =============================================
*/
CREATE PROCEDURE [dbo].FI_GetEntries
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
	declare @Offset bigint = 10000000000 * ' + convert(varchar(4), @Chunk) + '
	declare @StartEntryNo bigint = @Offset 
	declare @EndEntryNo   bigint = @Offset + 2147483647

	select
		S.[Key],
		d.[Entry No_],
		cast(d.[Posting Date] as date) as [Posting Date]
		,fi2.[Deal ID]
		,d.[FI No_]
		--,Null as [Local Currency Code]
		--,Null as [Additional Currency Code]
		--,dim1.[Dimension Value Code] as ACCRUAL_TYPE
		--,dim2.[Dimension Value Code] as Bank_Account
		--,dim3.[Dimension Value Code] as Book
		--,dim4.[Dimension Value Code] as Commissions
		--,dim5.[Dimension Value Code] as Costcenter
		--,dim6.[Dimension Value Code] as Counterparty
		--,dim7.[Dimension Value Code] as Currency
		--,dim8.[Dimension Value Code] as IC
		--,dim9.[Dimension Value Code] as Income_tax
		--,dim10.[Dimension Value Code] as Principal_Interest
		--,dim11.[Dimension Value Code] as Project
		--,dim12.[Dimension Value Code] as Provisions
		--,dim13.[Dimension Value Code] as Counterparty_Real
		,CASE 
			WHEN d.[Entry Type] = 0 THEN ''COST''
			WHEN d.[Entry Type] = 1 and fi2.[FI Entry Type] <> 2 THEN ''ALLOC''
			WHEN d.[Entry Type] = 1 and fi2.[FI Entry Type] = 2 THEN ''ALLOC.SALE''
			WHEN d.[Entry Type] = 2 and d.[Applied FI Ledger Entry No_] = 0 THEN ''MTM''
			WHEN d.[Entry Type] = 2 and d.[Applied FI Ledger Entry No_] <> 0 THEN ''MTM.Real''
			WHEN d.[Entry Type] = 3 THEN ''IPV''
		 END as Reval_Type
		--,dv.[Parent Code] as [DESK]
		,fi2.[External System ID]
		,fi2.[User ID]
		,convert(varchar(20), REPLACE(REPLACE(REPLACE(nullif(FI.[Customer No_],''''), ''"'', ''''), '''''''', ''''), ''&'', ''-'')) AS [Issuer No_]             
		--,NULL as [LE Code]
		,fi2.[Created Date]

		,cast(d.[Quantity] as decimal(28,5)) as [Quantity FI]
		,cast(d.[Sales Amount] as decimal(28,5)) as [OCY FI Sales Amount]
		,cast(d.[Sales Amount (LCY)] as decimal(28,5)) as [LCY FI Sales Amount]
		,cast(d.[Sales Amount (ACY)] as decimal(28,5)) as [ACY FI Sales Amount]
		,cast(d.[Cost Amount] as decimal(28,5)) as [OCY FI Cost Amount]
		,cast(d.[Cost Amount (LCY)] as decimal(28,5)) as [LCY FI Cost Amount]
		,cast(d.[Cost Amount (ACY)] as decimal(28,5)) as [ACY FI Cost Amount]
		,cast(d.[Sales Amount]+d.[Cost Amount] as decimal(28,5)) as [OCY FI Amount]
		,cast(d.[Sales Amount (LCY)]+d.[Cost Amount (LCY)] as decimal(28,5)) as [LCY FI Amount]
		,cast(d.[Sales Amount (ACY)]+d.[Cost Amount (ACY)] as decimal(28,5)) as [ACY FI Amount]

		/*
		,NULL as [OCY FI Sales Balance]
		,NULL as [LCY FI Sales Balance]
		,NULL as [ACY FI Sales Balance]
		,NULL as [OCY FI Cost Balance]
		,NULL as [LCY FI Cost Balance]
		,NULL as [ACY FI Cost Balance]
		,NULL as [OCY FI Balance]
		,NULL as [LCY FI Balance]
		,NULL as [ACY FI Balance]
		,NULL as [Quantity FI Balance]
		*/

	from
		' + @ReadTable +' as S WITH(Readuncommitted) 

		inner join [' + @Oltp + '].[dbo].[' + @CompanyName + '$Detailed FI Ledger Entry] as D	WITH(Readuncommitted) 
		on D.[Entry No_] = S.[Key] - @Offset

		LEFT JOIN [' + @Oltp + '].dbo.[' + @CompanyName + '$FI Ledger Entry 2] fi2
		ON D.[FI Entry No_] = fi2.[Entry No_]

		--LEFT OUTER JOIN [Dimension Value] as dv on dv.[Dimension Code]=''BOOK'' and dv.[Code]=dim3.[Dimension Value Code] and dv.[Parent Code] != ''''
		LEFT OUTER JOIN [' + @Oltp + '].[dbo].[Financial Instrument] AS FI ON  D.[FI No_] = FI.No_
	where	
		S.[Key] between @StartEntryNo and @EndEntryNo
		--and d.[Entry No_] = 1183720
	order by S.[Key]';

	--print(@Script)
	
	exec(@Script)
	with result sets
	(
		(
			[Key] BIGINT,
			[Entry No_] int,
			[Posting Date] DATE,
			[Deal ID] VARCHAR (20),
			[FI No_] VARCHAR (20),
			Reval_Type VARCHAR (10),
			[External System ID] VARCHAR (2),
			[User ID] VARCHAR (20),
			[Issuer No_] VARCHAR (20),
			[Created Date] DATETIME,
			[Quantity FI] DECIMAL (28, 5),
			[OCY FI Sales Amount] DECIMAL (28, 5),
			[LCY FI Sales Amount] DECIMAL (28, 5),
			[ACY FI Sales Amount] DECIMAL (28, 5),
			[OCY FI Cost Amount] DECIMAL (28, 5),
			[LCY FI Cost Amount] DECIMAL (28, 5),
			[ACY FI Cost Amount] DECIMAL (28, 5),
			[OCY FI Amount] DECIMAL (28, 5),
			[LCY FI Amount] DECIMAL (28, 5),
			[ACY FI Amount] DECIMAL (28, 5)
		)
	)
end
