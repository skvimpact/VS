/*
drop procedure [dbo].A_GetEntries
>>>>>>>>>> Test >>>>>>>>>>
exec [dwh].[A_GetEntries] 'TD Investments', 2, 1, 0
exec [dwh].[A_GetEntries] '_IFRS', 1, 1, 0
<<<<<<<<<< Test <<<<<<<<<<
*/
CREATE PROCEDURE [dbo].A_GetEntries
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
		'[dbo].[FactA_' + @CompanyName + '_' +  convert(varchar(3), @Partition) + ']' 

	--set @ReadTable  = '##FactA_' +  convert(varchar(3), @Partition) 

	set @Script = 
	'
	declare @CurCode varchar(20) = (select [LCY Code] from [' + @Oltp + '].dbo.[' + @CompanyName + '$General Ledger Setup])

	declare @Offset bigint = 10000000000 * ' + convert(varchar(10), @Chunk) + '
	declare @StartEntryNo bigint = @Offset 
	declare @EndEntryNo   bigint = @Offset + 2147483647

	select
		S.[Key],
		d.[Entry No_] , 
		d.[G_L Account No_] , 
		cast(d.[Posting Date] as date) as [Posting Date],
		cast(d.[Document Date] as date) [Document Date],
		d.[Document No_] , 
		convert(varchar(50), nullif(LTRIM(RTRIM(REPLACE(REPLACE(d.Description, CHAR(10), ''''), CHAR(13), ''''))), '''')) as [Description], 
		cast(d.[Amount] as decimal(28,5)) as [Amount], 
		d.[User ID] , 
		d.[Source Code] , 
		cast(d.[Quantity] as decimal(28,5)) as [Quantity], 
		d.[Reason Code] , 
		cast(d.[Debit Amount] as decimal(28,5)) as [Debit Amount], 
		cast(d.[Credit Amount] as decimal(28,5)) as [Credit Amount], 
		d.[Source Type] , 
		convert(varchar(20), REPLACE(REPLACE(REPLACE(d.[Source No_], ''"'', ''''), '''''''', ''''), ''&'', ''-'')) as [Source No_] , 
		cast(d.[Additional-Currency Amount] as decimal(28,5)) as [Additional-Currency Amount], 
		cast(d.[Add_-Currency Debit Amount] as decimal(28,5)) as [Add_-Currency Debit Amount], 
		cast(d.[Add_-Currency Credit Amount] as decimal(28,5)) as [Add_-Currency Credit Amount], 
		d.[Transaction Type] , 
		CASE d.Elimination WHEN 0 THEN ''No'' WHEN 1 THEN ''Yes'' END  AS [Elimination] , 
		nullif(d.[External System ID], '''') AS [External System ID] ,
		cast(iif(D.[Due Date] in (''17530101'', ''19000101''), Null, D.[Due Date]) as date) as [Due Date],
		nullif(d.[External Document ID],  '''') AS [External Document ID] , 
		cast(d.[Original Amount] as decimal(28,5)) as [Original Amount], 
		cast(d.[Original Debit Amount] as decimal(28,5)) as [Original Debit Amount], 
		cast(d.[Original Credit Amount] as decimal(28,5)) as [Original Credit Amount], 
		convert(varchar(20), REPLACE(REPLACE(d.[Local G_L Account], ''"'', ''''), '''''''', '''')) as [Local G_L Account] , 
		d.[Historical Date] , 
		CASE [Balance Entry] WHEN 0 THEN ''No'' WHEN 1 THEN ''Yes'' ELSE NULL END AS [Balance Entry] , 
		cast(d.[Debit Quantity] as decimal(28,5)) as [Debit Quantity], 
		cast(d.[Credit Quantity] as decimal(28,5)) as [Credit Quantity],  
		
			iif (' + convert(varchar(2), @Company) + ' <> 1,
				iif(D.[Source G_L Account No_] = '''', D.[G_L Account No_], D.[Source G_L Account No_]),
				iif(D.[Cons_ Time] < ''19000103'',
					D.[Source G_L Account No_],
					iif(D.[Source G_L Account No_] = '''', D.[G_L Account No_], D.[Source G_L Account No_])
				)
			) AS [Source G_L Account No_],

		cast(iif(datepart(hh, D.[Posting Date])= 0, 0,1) as tinyint) as [IsClosingDate],

		cast(iif(D.[Posted Time] in (''17530101'', ''19000101''), Null, dateadd(hour, 3, D.[Posted Time])) as date) as [Posted Time],
		cast(iif(D.[Cons_ Time] in (''17530101'', ''19000101''),  Null, dateadd(hour, 3, D.[Cons_ Time])) as date) as [Cons_ Time],

		iif([Original Currency Code] = '''', @CurCode, [Original Currency Code]) as [Original Currency Code],

		[Business Unit Code] as [Source LE Code],
		cast(hashbytes(''md5'', d.[Document No_]) as bigint) as [SK_Document], --NULL, --Doc.id,
		cast(hashbytes(''md5'', nullif(d.[External Document ID],'''')) as bigint) as [SK_ExtDocument], --50 NULL, --ExtDoc.id,
		cast(hashbytes(''md5'', nullif(LTRIM(RTRIM(REPLACE(REPLACE(d.Description, CHAR(10), ''''), CHAR(13), ''''))),'''')) as bigint) as [SK_Description], --NULL,--Descr.id,
		cast(iif( CD.[Closing Date] in (''17530101'', ''20501231'', ''19000101''), Null, CD.[Closing Date]) as date) as [Closing Date]
		,CASE WHEN d.[Source Type] = 2 THEN d.[Source No_] ELSE NULL END AS [Vendor No]
		,CASE WHEN d.[Source Type] = 1 THEN d.[Source No_] ELSE NULL END AS [Customer No]
		,d.[External Document No_]
		,d.[Business Unit Code]
		,convert(varchar(20), REPLACE(REPLACE([Source G_L Account No_], ''"'', ''''), '''''''', '''')) [Source G_L Account No_ 2]
		 ,d.[Allocation Dimension 1 Code] as [Allocation COSTCENTER]
		 ,d.[Allocation Step]
		 ,d.[Entry No_] as DimGLAllocId
		 ,cast(d.[Allocation Base] as decimal(28,5)) as [Allocation Base]
		 ,cast(d.[Allocation Value] as decimal(28,5)) as [Allocation Value]
		 ,cast(hashbytes(''md5'', nullif(d.[External Document No_],'''')) as bigint) as [SK_ExtenalDocumentNO] --20 --,NULL --,ExtDocNO.id
	from
		[' + @Oltp + '].[dbo].[' + @CompanyName + '$Allocation of Cost G_L Entry] as d	WITH(nolock) 
		
		inner join ' + @ReadTable +' as S WITH(nolock) 
		on d.[Entry No_] = S.[Key] - @Offset

	    left OUTER JOIN    [' + @Oltp+ '].dbo.[Operation Closing] AS CD ON d.[External Document ID] = CD.[Document ID]    

	where	
		S.[Key] between @StartEntryNo and @EndEntryNo
	order by S.[Key]';

--	print(@Script)
	
	exec(@Script)
	with result sets
	(
		(
			[Key] BIGINT,
			[Entry No_] INT,
			[G_L Account No_] VARCHAR (20),
			[Posting Date] DATE,
			[Document Date] DATE,
			[Document No_] VARCHAR (20),
			[Description] VARCHAR (50),
			Amount DECIMAL (28, 5),
			[User ID] VARCHAR (20),
			[Source Code] VARCHAR (10),
			Quantity DECIMAL (28, 5),
			[Reason Code] VARCHAR (10),
			[Debit Amount] DECIMAL (28, 5),
			[Credit Amount] DECIMAL (28, 5),
			[Source Type] INT,
			[Source No_] VARCHAR (20),
			[Additional-Currency Amount] DECIMAL (28, 5),
			[Add_-Currency Debit Amount] DECIMAL (28, 5),
			[Add_-Currency Credit Amount] DECIMAL (28, 5),
			[Transaction Type] INT,
			Elimination VARCHAR (3),
			[External System ID] VARCHAR (2),
			[Due Date] DATE,
			[External Document ID] VARCHAR (50),
			[Original Amount] DECIMAL (28, 5),
			[Original Debit Amount] DECIMAL (28, 5),
			[Original Credit Amount] DECIMAL (28, 5),
			[Local G_L Account] VARCHAR (20),
			[Historical Date] DATETIME,
			[Balance Entry] VARCHAR (3),
			[Debit Quantity] DECIMAL (28, 5),
			[Credit Quantity] DECIMAL (28, 5),
			[Source G_L Account No_] VARCHAR (20),
			IsClosingDate TINYINT,
			[Posted Time] DATE,
			[Cons_ Time] DATE,
			[Original Currency Code] VARCHAR (10),
			[Source LE Code] VARCHAR (20),
			SK_Document BIGINT,
			SK_ExtDocument BIGINT,
			SK_Description BIGINT,
			[Closing Date] DATE,
			[Vendor No] VARCHAR (20),
			[Customer No] VARCHAR (20),
			[External Document No_] VARCHAR (20),
			[Business Unit Code] VARCHAR (20),
			[Source G_L Account No_ 2] VARCHAR (20),
			[Allocation COSTCENTER] VARCHAR (20),
			[Allocation Step] VARCHAR (20),
			DimGLAllocId INT,
			[Allocation Base] DECIMAL (28, 5),
			[Allocation Value] DECIMAL (28, 5),
			SK_ExtenalDocumentNO BIGINT
		)
	)
end;