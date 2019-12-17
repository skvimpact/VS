/*
drop procedure [dbo].GL_GetEntries
Test >>> =============================================
exec [dbo].[GL_GetEntries] 'IC Troika Dialog', 3, 14, 0, 'Nav-Offshores-Day'
exec [dbo].[GL_GetEntries] 'ID Investments', 2, 37, 4, 'Nav-Offshores-Day'



exec [dbo].[GL_GetEntries] '_IFRS', 1, 77, 0, 'Nav-Offshores-Day'
exec [dbo].[OltpReadSchema_GetInsertCommand] 1, 1, 77, 'Nav-Offshores-Day'


Test <<< =============================================
*/
alter PROCEDURE [dbo].GL_GetEntries
(
	@CompanyName	varchar(30),
	@Company		int,
	@Partition		int,
	@Chunk			int,
	@Oltp			varchar(30)
)
as begin
	set nocount on;
	declare @Script varchar(max)
	declare @ReadTable varchar(50) = 
		'[dbo].[FactGL_' + @CompanyName + '_' +  convert(varchar(3), @Partition)+ ']' 

	--set @ReadTable  = '[dbo].[##FactGL_' +  @CompanyName + '_' + convert(varchar(3), @Partition) + '_A]'
	--set @ReadTable  = '[dbo].[#FactGL_' + convert(varchar(3), @Partition) + '_A]'
	set @Script = 
	'
	declare @CurCode varchar(20) = (select [LCY Code] from [' + @Oltp + '].dbo.[' + @CompanyName + '$General Ledger Setup])

	declare @Offset bigint = 10000000000 * ' + convert(varchar(10), @Chunk) + '
	declare @StartEntryNo bigint = @Offset 
	declare @EndEntryNo   bigint = @Offset + 2147483647

	select
		S.[Key],
		--iif(@LECode = ''_IFRS'', [Business Unit Code], @LECode) as [Deal Le Code],
		--convert(varchar(6), G.[Posting Date], 112) as [Part Id],
		cast(G.[timestamp] as bigint) as [Timespan],
		G.[Entry No_] as [Entry No_],
		G.[G_L Account No_] as [G_L Account No_], 
		nullif(convert(varchar(20), REPLACE(REPLACE(G.[Local G_L Account], ''"'', ''''), '''''''', '''')),'''') as [Local G_L Account],

		cast(G.[Posting Date] as date) as [Posting Date],
		cast(G.[Document Date] as date) as [Document Date],
		--[Document No_],

		[Document No_] as Document,
		--nullif(G.[External Document No_], '''') as [External Document No_],
		nullif(G.[External Document ID], '''') as [External Document],
		--nullif(LTRIM(RTRIM(REPLACE(REPLACE(G.Description, CHAR(10), ''''), CHAR(13), ''''))), '''') as Description,

		cast(hashbytes(''md5'', [Document No_]) as bigint) as [DocumentNoId],
		--cast(hashbytes(''md5'', nullif(G.[External Document No_], '''')) as bigint) as [SkExternalDocumentNo],
		cast(hashbytes(''md5'', nullif(G.[External Document No_],'''')) as bigint) as [ExtDocNoId],
		cast(hashbytes(''md5'', nullif(G.[External Document ID],'''')) as bigint) as [ExtDocId],
		cast(hashbytes(''md5'', nullif(LTRIM(RTRIM(REPLACE(REPLACE(G.Description, CHAR(10), ''''), CHAR(13), ''''))),'''')) as bigint) as [DescriptionId],

		G.[User ID] as [User ID],
		nullif(G.[Source Code], '''') as [Source Code],
		nullif(G.[Reason Code], '''') as [Reason Code], 

		cast(G.[Amount] as decimal(28,5)) as [Amount], 	
		cast(G.[Debit Amount] as decimal(28,5)) as [Debit Amount], 
		cast(G.[Credit Amount] as decimal(28,5)) as [Credit Amount], 
		cast(G.[Additional-Currency Amount] as decimal(28,5)) as [Additional-Currency Amount], 
		cast(G.[Add_-Currency Debit Amount] as decimal(28,5)) as [Add_-Currency Debit Amount], 
		cast(G.[Add_-Currency Credit Amount] as decimal(28,5)) as [Add_-Currency Credit Amount], 
		cast(G.[Original Amount] as decimal(28,5)) as [Original Amount], 
		cast(G.[Original Debit Amount] as decimal(28,5)) as [Original Debit Amount], 
		cast(G.[Original Credit Amount] as decimal(28,5)) as [Original Credit Amount], 	
		cast(G.[Quantity] as decimal(28,5)) as [Quantity], 
		cast(G.[Debit Quantity] as decimal(28,5)) as [Debit Quantity], 
		cast(G.[Credit Quantity] as decimal(28,5)) as [Credit Quantity], 

		cast(G.[Source Type] as tinyint) as [Source Type], 
		nullif(convert(varchar(20), REPLACE(REPLACE(REPLACE(G.[Source No_], ''"'', ''''), '''''''', ''''), ''&'', ''-'')),'''') as [Source No_] , 

		cast(G.[Transaction Type] as tinyint) as [Transaction Type], 	
		cast(G.Elimination as tinyint) as [Elimination],

		nullif(G.[External System ID], '''') as [External System ID],	

		cast(iif(G.[Due Date] in (''17530101'', ''20501231'', ''19000101''), Null, G.[Due Date]) as date) as [Due Date],	
		cast(G.[Historical Date] as date) as [Historical Date],

		cast(G.[Special Correspondense] as tinyint) as [Special Correspondense],
		cast(G.[Balance Entry] as tinyint) AS [Balance Entry] , 

/*		nullif(
			iif (' + convert(varchar(2), @Company) + ' <> 1,
				iif(G.[Source G_L Account No_] = '''', G.[G_L Account No_], G.[Source G_L Account No_]),
				iif(G.[Cons_ Time] < ''19000103'',
					G.[Source G_L Account No_],
					iif(G.[Source G_L Account No_] = '''', G.[G_L Account No_], G.[Source G_L Account No_])
				)
			), '''') AS [Source G_L Account No_],*/

		
		iif (' + convert(varchar(2), @Company) + ' <> 1,
			iif(G.[Source G_L Account No_] = '''', G.[G_L Account No_], G.[Source G_L Account No_]),
			iif(G.[Cons_ Time] < ''19000103'',
				G.[Source G_L Account No_],
				iif(G.[Source G_L Account No_] = '''', G.[G_L Account No_], G.[Source G_L Account No_])
			)
		) AS [Source G_L Account No_],



		cast(iif(datepart(hh, G.[Posting Date])= 0, 0,1) as tinyint) as [IsClosingDate],


		cast(iif(G.[Posted Time] in (''17530101'', ''19000101''), Null, dateadd(hour, 3, G.[Posted Time])) as date) as [Posted Time],
		cast(iif(G.[Cons_ Time] in (''17530101'', ''19000101''),  Null, dateadd(hour, 3, G.[Cons_ Time])) as date) as [Cons_ Time],


		iif([Original Currency Code] = '''', @CurCode, [Original Currency Code]) as [Original Currency Code],
		--null  as [Local Currency Code],
		--null as [Additional Currency Code],

		nullif([Original System ID], '''') as [Original System ID], -- "External System"
		cast(iif( CD.[Closing Date] in (''17530101'', ''20501231'', ''19000101''), Null, CD.[Closing Date]) as date) as [Closing Date],
		--null as [Closing Date],
	 

		iif( G.[Source Type] = 2, nullif(REPLACE(REPLACE(REPLACE(G.[Source No_], ''"'', ''''), '''''''', ''''), ''&'', ''-''),''''), NULL ) AS [Vendor No],
		iif( G.[Source Type] = 1, nullif(REPLACE(REPLACE(REPLACE(G.[Source No_], ''"'', ''''), '''''''', ''''), ''&'', ''-''),''''), NULL ) AS [Customer No],
	
		nullif([Business Unit Code], '''') as [Business Unit Code],
		nullif([Source G_L Account No_], '''') [Source G_L Account No_ 2],		
		--src_acc.Name as [Source G_L Account Name],	
		--null           as [Source G_L Account Name],
		cast(iif(G.[Source Posting Date] = ''17530101'', Null, G.[Source Posting Date]) as date) as [Source Posting Date]
	from
		[' + @Oltp + '].[dbo].[' + @CompanyName + '$G_L Entry] as G	WITH(nolock) 
		
		inner join ' + @ReadTable +' as S WITH(nolock) 
		on G.[Entry No_] = S.[Key] - @Offset

		left OUTER JOIN [' + @Oltp + '].dbo.[Operation Closing] AS CD 
		ON CD.[Document ID]  = G.[External Document ID] 
	where	
		S.[Key] between @StartEntryNo and @EndEntryNo


		--and G.[Entry No_] = 47886189


	order by S.[Key]';

	--print(@Script)
	
	exec(@Script)
	with result sets
	(
		(
			[Key] BIGINT,
			--[Part Id] VARCHAR (6),
			[Timespan] BIGINT,
			[Entry No_] INT,
			[G_L Account No_] VARCHAR (20),
			[Local G_L Account] VARCHAR (20),
			[Posting Date] DATE,
			[Document Date] DATE,
			--[Document No_] VARCHAR (20),

			[Document] VARCHAR (20),
			--[External Document No_] VARCHAR (20),
			[External Document] VARCHAR (50),
			--[Description] VARCHAR (50),


			DocumentNoId BIGINT,			
			ExtDocNoId BIGINT,
			ExtDocId BIGINT,
			DescriptionId BIGINT,
			[User ID] VARCHAR (20),
			[Source Code] VARCHAR (10),
			[Reason Code] VARCHAR (10),
			Amount DECIMAL (28, 5),
			[Debit Amount] DECIMAL (28, 5),
			[Credit Amount] DECIMAL (28, 5),
			[Additional-Currency Amount] DECIMAL (28, 5),
			[Add_-Currency Debit Amount] DECIMAL (28, 5),
			[Add_-Currency Credit Amount] DECIMAL (28, 5),
			[Original Amount] DECIMAL (28, 5),
			[Original Debit Amount] DECIMAL (28, 5),
			[Original Credit Amount] DECIMAL (28, 5),
			Quantity DECIMAL (28, 5),
			[Debit Quantity] DECIMAL (28, 5),
			[Credit Quantity] DECIMAL (28, 5),
			[Source Type] TINYINT,
			[Source No_] VARCHAR (20),
			[Transaction Type] TINYINT,
			Elimination TINYINT,
			[External System ID] VARCHAR (2),
			[Due Date] DATE,
			[Historical Date] DATE,
			[Special Correspondense] TINYINT,
			[Balance Entry] TINYINT,
			[Source G_L Account No_] VARCHAR (20),
			IsClosingDate TINYINT,
			[Posted Time] DATE,
			[Cons_ Time] DATE,
			[Original Currency Code] VARCHAR (5),
			[Original System ID] VARCHAR (2),
			[Closing Date] DATE,
			[Vendor No] VARCHAR (20),
			[Customer No] VARCHAR (20),
			[Business Unit Code] VARCHAR (20),
			[Source G_L Account No_ 2] VARCHAR (20),
			[Source Posting Date] DATE
		)
	)
end;