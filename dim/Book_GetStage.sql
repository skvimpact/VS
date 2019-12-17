-- drop PROCEDURE [dwh].[Book_GetStage]
-- exec [dwh].[Book_GetStage]
CREATE PROCEDURE [dwh].[Book_GetStage]
AS begin
	set nocount on;
	SELECT     
		d.Code, 
		d.Name, 
		d.[External Code] AS [MIS Code], 
		mc.[Costcenter Name] AS [MIS Name],
		b.[Business Model]	 as [Business Model],
		nullif(b.[Portfolio Code],'') as [Portfolio Code],
		[Parent Code]
	FROM         
		[Dimension Value] AS d 
		LEFT OUTER JOIN
			[MIS Costcenter] AS mc ON 
				d.[External Code] = mc.[Costcenter Code]
		left join 
			[Book] as b on
				b.Code = d.Code
	WHERE     
		(d.[Dimension Code] = 'BOOK') 
		AND (d.[Dimension Value Type] <> 4)	
end
