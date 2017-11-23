using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
namespace MDX_Script_Executor
{
    public class MDXScriptExecutor
    {
        private AdomdConnection conn;

        public MDXScriptExecutor(string connection)
        {
            conn = new AdomdConnection(connection);
            try
            {
                conn.Open();
            }
            catch (AdomdConnectionException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void Run<T>(string mdxScript, ICollection<T> result, Func<AdomdDataReader, T> mapper) 
        {
            
            try
            {
                AdomdCommand cmd = new AdomdCommand(mdxScript, conn);
                using (AdomdDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(mapper(dr));                                
                    }
                }
            }
            catch (AdomdErrorResponseException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Run<TKey, TValue>(string mdxScript, IDictionary<TKey, TValue> result, Func<AdomdDataReader, String[], TKey> mapperKey, Func<AdomdDataReader, TValue> mapperValue, String[] args)
        {
            
            try
            {
                AdomdCommand cmd = new AdomdCommand(mdxScript, conn);
                using (AdomdDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(mapperKey(dr, args), mapperValue(dr));
                    }
                }
            }
            catch (AdomdErrorResponseException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}