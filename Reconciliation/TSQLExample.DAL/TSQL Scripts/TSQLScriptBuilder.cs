using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQLExample.DAL
{
    public partial class TSQLScriptBuilder
    {
        static public string getCompanyList()
        {
            StringBuilder myStringBuilder = new StringBuilder();

            myStringBuilder.Append("select");
            myStringBuilder.Append("	[Name]");
            myStringBuilder.Append("from");
            myStringBuilder.Append("	[dbo].[Company]");

            return myStringBuilder.ToString();
        }
    }
}
