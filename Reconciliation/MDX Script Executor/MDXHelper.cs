using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDX_Script_Executor
{

    public class WrongMonthException : ApplicationException
    {
        private int month;

        public WrongMonthException(int month)
        {
            this.month = month;
        }

        public override string Message
        {
            get
            {
                return string.Format("Illegal month number: {0}", month);
            }
        }

    }

    public class MDXHelper
    {
        static private string[] months = {
                                "January",
                                "February",
                                "March",
                                "April",
                                "May",
                                "June",
                                "July",
                                "August",
                                "September",
                                "October",
                                "November",
                                "December"
                              };

        static public string getMonthName(int month)
        {
            if ((month < 1) || (month > 12)) throw new WrongMonthException(month);
            return months[month - 1];
        }
    }

}
