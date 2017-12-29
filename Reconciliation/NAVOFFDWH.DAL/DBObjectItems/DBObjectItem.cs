using System;
using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace NAVOFFDWH_DAL
{
    public abstract class DBObjectItem
    {
//        protected string DBObjectItemType { get; set; }
        public object[] GetValues()
        {
            ArrayList objects = new ArrayList();
            foreach (var property in this.GetType().GetProperties())
            {
                if (((ColumnAttribute)property.GetCustomAttributes(false).Where(x => x is ColumnAttribute).FirstOrDefault()) != null)
                    objects.Add(property.GetValue(this));
                
            }
            return objects.ToArray();
        }


        public void SetValue()
        {
            int index = 0;
            foreach (var property in this.GetType().GetProperties())
            {
                if (((ColumnAttribute)property.GetCustomAttributes(false).Where(x => x is ColumnAttribute).FirstOrDefault()) != null)
                {
                    if (index == 0)
                        property.SetValue(this, "Reflection value");
                    index++;                   //
                }
            }
        }
    }
}
