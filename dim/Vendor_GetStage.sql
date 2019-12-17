-- exec [dwh].[Vendor_GetStage]
CREATE PROCEDURE [dwh].[Vendor_GetStage]
AS begin
	set nocount on;
	SELECT     
		[No_],
		[Name],
		[Counterparty Category]
	FROM      
		dbo.Vendor
end
