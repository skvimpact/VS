/*
drop procedure [dbo].[V_GetEntries]
Go
-- =============================================
-- Test: exec [dwh].[V_GetEntries] 'TD Investments', 2, 1, 0
-- Test: exec [dwh].[V_GetEntries] 'TD Investments', 2, 1, 1
*/
-- =============================================

CREATE PROCEDURE [dbo].[V_GetEntries]
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
	declare @CurCode varchar(20) = (select [LCY Code] from [' + @Oltp + '].dbo.[' + @CompanyName + '$General Ledger Setup])

	declare @Offset bigint = 10000000000 * ' + convert(varchar(10), @Chunk) + '
	declare @StartEntryNo bigint = @Offset 
	declare @EndEntryNo   bigint = @Offset + 2147483647

	select
		S.[Key],

		d.[Entry No_] , 
		REPLACE(REPLACE(REPLACE(d.[Vendor No_], ''"'', ''''), '''''''', ''''), ''&'', ''-'') as [Vendor No_] , 
		d.[Document No_] , 
		case when LTRIM(RTRIM(REPLACE(REPLACE(d.[Description], CHAR(10), ''''), CHAR(13), '''') ))<>'''' then LTRIM(RTRIM(REPLACE(REPLACE(Replace(Replace(d.[Description],''"'',''''),'''''''',''''), CHAR(10), ''''), CHAR(13), '''') )) else Null end as [Description] , 
		case when d.[Due Date] = ''1753/01/01'' or  d.[Due Date] = ''1900/01/01'' then Null else d.[Due Date] end as [Due Date] , 
		case when d.[Closed at Date] = ''1753/01/01'' or  d.[Closed at Date] = ''1900/01/01'' then Null else d.[Closed at Date] end as  [Closed at Date] , 
		case when d.[Bal_ Account No_]= '''' then Null else d.[Bal_ Account No_] end  as [Bal_ Account No_] , 
		d.[Transaction No_] , 
		--d.[External Document No_] , 
		case d.[External System ID] when '''' then NULL else d.[External System ID] end as [External System ID] ,
		--''[#LE CODE]'' as [LE Code] ,
		--''[#LOC CUR CODE]'' as [Local Currency Code] ,
		--''[#ADD CUR CODE]'' as [Additional Currency Code], 
		dvle.[Entry No_] as DVLEEntryNo, 
		dvle.[Posting Date] AS [Posting Date],
		case when datepart(hh,dvle.[Posting Date])= 0 then 0 else 1 end as IsClosingDate, 
		--case dvle.[Currency Code] when '''' then ''[#CUR CODE]'' else dvle.[Currency Code] end as [Currency Code],
		iif(dvle.[Currency Code] = '''', @CurCode, dvle.[Currency Code]) as [Currency Code],
		dvle.[User ID], 
		dvle.[Amount],
		dvle.[Amount (LCY)]
	    ,gl.[Entry No_] as GLEntryNo
	    ,gl.[G_L Account No_]
	    ,REPLACE(REPLACE(gl.[Local G_L Account], ''"'', ''''), '''''''', '''') as [Local G_L Account]
	    ,gl.[Transaction Type]
	    ,case when gl.[Posted Time] in (''1753/01/01'',''1900/01/01'') then null else  cast(convert(nvarchar, dateadd(hour, 3,  gl.[Posted Time]), 112) as date) end as [Posted Time]
	    ,case when gl.[Cons_ Time] in (''1753/01/01'',''1900/01/01'')  then null else  cast(convert(nvarchar, dateadd(hour, 3,  gl.[Cons_ Time]), 112) as date) end as [Cons_ Time]
	    ,CASE gl.[Balance Entry] when 0 then ''No'' When 1 then ''Yes'' else Null end as [Balance Entry]
	    ,CASE when (gl.[Source G_L Account No_]='''') or (gl.[Source G_L Account No_]is Null) then  REPLACE(REPLACE(gl.[G_L Account No_], ''"'', ''''), '''''''', '''')  else  REPLACE(REPLACE(gl.[Source G_L Account No_], ''"'', ''''), '''''''', '''') end as [Source Account]
	    ,gl.[Historical Date]
	    ,case gl.Elimination when 0 then ''No'' else ''Yes'' end as Elimination
	    ,cast(hashbytes(''md5'', d.[Document No_]) as bigint) as [SK_Document] --,NULL --,Doc.id
	    ,cast(hashbytes(''md5'', nullif(LTRIM(RTRIM(REPLACE(REPLACE(d.Description, CHAR(10), ''''), CHAR(13), ''''))),'''')) as bigint) as [SK_Description] --,NULL --,Descr.id,

		--,case when FI.[No_]='''' or (FI.[No_]is Null) then ''NA'' else FI.[No_] END as [FI]


	from
		[' + @Oltp + '].[dbo].[' + @CompanyName + '$Vendor Ledger Entry] as D	WITH(nolock) 

		inner join ' + @ReadTable +' as S WITH(nolock) 
		on D.[Entry No_] = S.[Key] - @Offset
		
		inner join [' + @Oltp + '].[dbo].[' + @CompanyName + '$Detailed Vendor Ledg_ Entry] as dvle	WITH(nolock) 			
		on D.[Entry No_] = dvle.[Vendor Ledger Entry No_] 

		left outer join [' + @Oltp + '].[dbo].[' + @CompanyName + '$G_L Entry] as GL	WITH(nolock) 
		on d.[Entry No_] = gl.[Entry No_]

		--left OUTER JOIN       [Financial Instrument] AS FI ON dim26.[Dimension Value Code] = FI.No_ 

		--left OUTER JOIN dwh.[Operation Closing] AS CD 
		--ON CD.[Document ID]  = G.[External Document ID] 

	    --left OUTER JOIN dwh.[Sberbank Switzerland AG$FiRe G_L Account] src_acc 
		--on src_acc.[No_] = G.[Source G_L Account No_]

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
			[Vendor No_] VARCHAR (20),
			[Document No_] VARCHAR (20),
			[Description] VARCHAR (50),
			[Due Date] DATETIME,
			[Closed at Date] DATETIME,
			[Bal_ Account No_] VARCHAR (20),
			[Transaction No_] INT,
			--[External Document No_] VARCHAR (20),
			[External System ID] VARCHAR (2),
			DVLEEntryNo INT,
			[Posting Date] DATETIME,
			IsClosingDate INT,
			[Currency Code] VARCHAR (10),
			[User ID] VARCHAR (20),
			Amount DECIMAL (38, 20),
			[Amount (LCY)] DECIMAL (38, 20),
			GLEntryNo INT,
			[G_L Account No_] VARCHAR (20),
			[Local G_L Account] VARCHAR (20),
			[Transaction Type] INT,
			[Posted Time] DATE,
			[Cons_ Time] DATE,
			[Balance Entry] VARCHAR (3),
			[Source Account] VARCHAR (20),
			[Historical Date] DATETIME,
			Elimination VARCHAR (3),
			[SK_Document] BIGINT,
			[SK_Description] BIGINT
		)
	)
end;