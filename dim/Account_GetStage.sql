-- exec [dwh].[Account_GetStage]
-- drop PROCEDURE [dwh].[Account_GetStage]
CREATE PROCEDURE [dwh].[Account_GetStage]
AS begin
	set nocount on;
	SELECT     
		[No_],
		[Name],
		[GFO Duration Type],
		[Business Model],
		[SPPI Test]
	FROM         
		[dbo].[G_L Account]
end
