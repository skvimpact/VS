-- exec [dwh].[Costcenter_GetStage]
CREATE PROCEDURE [dwh].[Costcenter_GetStage]
AS begin
	set nocount on;
	SELECT     
		d.Code, 
		d.[Name]
	FROM         
		[Dimension Value] AS d 
	WHERE     
		(d.[Dimension Code] = 'COSTCENTER') 
		AND (d.[Dimension Value Type] <> 4)	
end
