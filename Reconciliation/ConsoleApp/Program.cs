using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[assembly: CLSCompliant(true)]
namespace ConsoleApp
{
    //  [Serializable]
    [Table("sss", Schema ="dbo")]
    [MyCool(Coolness = 5)]
    class PPP7
    {
        
        [MyCool(Coolness = 10)]
        public PPP7() { }

    }

    //[AttributeUsage(AttributeTargets.Class)]
    public class MyCoolAttribute : Attribute
    {
        public int Coolness { get; set; }
        public MyCoolAttribute(int coolness) => Coolness = coolness;
        public MyCoolAttribute() { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Type t = Type.GetType("ConsoleApp.PPP7");
            object[] customAtts = t.GetCustomAttributes(false);

            foreach (MyCoolAttribute v in customAtts.Where(x => x.GetType().Equals(typeof(MyCoolAttribute))))
            {
                Console.WriteLine("Coolness {0}\n", v.Coolness);
            }

            //foreach (TableAttribute v in customAtts.Where(x => x.GetType().Equals(typeof(TableAttribute))))
            foreach (TableAttribute v in customAtts.Where(x => x is TableAttribute))
            {
                Console.WriteLine($"Coolness [{v.Schema}].[{v.Name}]\n");
            }

            //var t2 = typeof(PPP7);


            Console.WriteLine(t?.Name);
            Console.ReadKey();
        }
    }
}
