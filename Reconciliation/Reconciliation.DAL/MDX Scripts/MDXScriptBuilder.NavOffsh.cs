using System.Text;
using Script_Executor;


namespace Reconciliation.DAL
{
    public partial class MDXScriptBuilder
    {

        static public string getNavOffshOriginalAccountList()
        {
            StringBuilder myStringBuilder = new StringBuilder();

            myStringBuilder.Append("with MEMBER [Measures].[0] as 0"); 
            myStringBuilder.Append("	select");
            myStringBuilder.Append("		[Measures].[0] on 0,");
            myStringBuilder.Append("		{[Original Account].[Original Account No].[Original Account No]} on 1");
            myStringBuilder.Append("	from");
            myStringBuilder.Append("		[CONS]");

            return myStringBuilder.ToString();
        }


        static public string getNavOffshGLInfo(int year, int month, string accountNo)
        {
            StringBuilder myStringBuilder = new StringBuilder();

            myStringBuilder.Append("with"); 
            myStringBuilder.Append("	member [Measures].[OCY Balance Calc] as");
            myStringBuilder.Append("	Aggregate(");	
            myStringBuilder.Append("			Null:[Posting Date].[Y-Q-M-D].CurrentMember,");
            myStringBuilder.Append("			([Measures].[OCY Amount])");
            myStringBuilder.Append("	)");
            myStringBuilder.Append("	member [Measures].[LCY Balance Calc] as");
            myStringBuilder.Append("	Aggregate(");	
            myStringBuilder.Append("			Null:[Posting Date].[Y-Q-M-D].CurrentMember,");
            myStringBuilder.Append("			([Measures].[LCY Amount])");
            myStringBuilder.Append("	)");
            myStringBuilder.Append("	select");
            myStringBuilder.Append("		{[Measures].[OCY Balance Calc], [Measures].[LCY Balance Calc], [Measures].[OCY Amount], [Measures].[LCY Amount]} on 0,");
            myStringBuilder.Append("		non empty crossjoin(");		
            myStringBuilder.Append("			{[Legal Entity].[Legal Entity Code].[Legal Entity Code]},");
            myStringBuilder.Append("			{[Bank Account].[Bank Account Code].[Bank Account Code]},");
            myStringBuilder.Append("			{[Book].[Book Code].[Book Code]},");
            myStringBuilder.Append("			{[Cost Center].[Cost Center Code].[Cost Center Code]},");
            myStringBuilder.Append("			{[Counterparty].[Counterparty Code].[Counterparty Code]},");
            myStringBuilder.Append("			{[Real Counterparty].[Counterparty Code].[Counterparty Code]},");
            myStringBuilder.Append("			{[Currency].[Currency Code].[Currency Code]},");
            myStringBuilder.Append("			{[Deal].[Deal ID].[Deal ID]},");
            myStringBuilder.Append("			{[IC].[IC Code].[IC Code]},");
            myStringBuilder.Append("			{[FA].[FA Code].[FA Code]},");
            myStringBuilder.Append("			{[Commission].[Commission Code].[Commission Code]},");
            myStringBuilder.Append("			{[Operating Expense].[Operating Expense Code].[Operating Expense Code]},");
            myStringBuilder.Append("			{[Provision].[Provision Code].[Provision Code]},");
            myStringBuilder.Append("			{[Movement].[Movement Code].[Movement Code]},");
            myStringBuilder.Append("			{[Reconcilation Type].[Reconcilation Type Code].[Reconcilation Type Code]},");
            myStringBuilder.Append("			{[Tax Jurisdiction].[Tax Jurisdiction Code].[Tax Jurisdiction Code]},");
            myStringBuilder.Append("			{[Tax].[Tax Code].[Tax Code]},");
            myStringBuilder.Append("			{[Income Tax].[Income Tax Code].[Income Tax Code]},");
            myStringBuilder.Append("			{[Principal Interest].[Principal Interest Code].[Principal Interest Code]},");
            myStringBuilder.Append("			{[Reval Type].[Reval Type Code].[Reval Type Code]},");
            myStringBuilder.Append("			{[Accrual Type].[Accrual Type Code].[Accrual Type Code]},");
            myStringBuilder.Append("			{[Project].[Project Code].[Project Code]},");
            myStringBuilder.Append("			{[GW Operation Type].[GW Operation Type Code].[GW Operation Type Code]},");
            myStringBuilder.Append("			{[FA Operation Type].[FA Operation Type Code].[FA Operation Type Code]},");
            myStringBuilder.Append("			{[Deffered Expense].[Deffered Expense Code].[Deffered Expense Code]},");
            myStringBuilder.Append("			{[Fund Status].[Fund Status Code].[Fund Status Code]},");
            myStringBuilder.Append("			{[Financial Instrument].[FI Code].[FI Code]},");
            myStringBuilder.Append("			{[Capital Move Type].[Capital Move Type Code].[Capital Move Type Code]}");
            myStringBuilder.Append("		) on 1");
            myStringBuilder.Append("	from");
            myStringBuilder.Append("		[CONS]");
            myStringBuilder.Append("where");
            myStringBuilder.Append("(");
            myStringBuilder.Append("    [Original Account].[Original Account No].&[" + accountNo + "],");
            myStringBuilder.Append("    [Posting Date].[Y-Q-M-D].[Month].&[" + year.ToString() + "]&[" + MDXHelper.getMonthName(month) + "]");
            myStringBuilder.Append(")");

            return myStringBuilder.ToString();
        }
    }
}
