-- exec [dwh].[Customer_GetStage]
CREATE PROCEDURE [dwh].[Customer_GetStage]
AS begin
	set nocount on;
	SELECT     
		[No_],
		[Name],
		[Counterparty Category]
	FROM      
		dbo.Customer
end
