-- exec [dwh].[FinancialInstrument_GetStage]
-- drop PROCEDURE [dwh].[FinancialInstrument_GetStage]
CREATE PROCEDURE [dwh].[FinancialInstrument_GetStage]
AS begin
	set nocount on;
	SELECT     
		[No_],
		[Name],
		[Type FI],
		[Customer No_]	
	FROM         
		[dbo].[Financial Instrument]
end
