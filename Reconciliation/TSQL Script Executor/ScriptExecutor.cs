using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Script_Executor
{
    public class ScriptExecutor
    {        
        private DbConnection _conn;
        private DbProviderFactory _factory;


        public ScriptExecutor(string connection, string providerName)
        {

            _factory = DbProviderFactories.GetFactory(providerName);
            
            _conn = _factory.CreateConnection();
            _conn.ConnectionString = connection;
            try
            {
                _conn.Open();
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Run<T>(string script, ICollection<T> result, Func<DbDataReader, T> mapper)
        {
            try
            {
                using (DbCommand cmd = _factory.CreateCommand())
                {
                    cmd.CommandText = script;
                    cmd.Connection = _conn;
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(mapper(dr));
                        }
                    }
                }
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
            }            
        }

        public void Run<T>(string procedure, ICollection<DbParameter> parameters, ICollection<T> result, Func<DbDataReader, T> mapper)
        {
            try
            {
                using (DbCommand cmd = _factory.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (DbParameter parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    cmd.CommandText = procedure;
                    cmd.Connection = _conn;

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(mapper(dr));
                        }
                    }
                }
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Run<TKey, TValue>(string script, IDictionary<TKey, TValue> result, Func<DbDataReader, String[], TKey> mapperKey, Func<DbDataReader, TValue> mapperValue, String[] args)
        {
            try
            {
                using (DbCommand cmd = _factory.CreateCommand())
                {
                    cmd.CommandText = script;
                    cmd.Connection = _conn;
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(mapperKey(dr, args), mapperValue(dr));
                        }
                    }
                }
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private void ddd()
        {
            DbDataAdapter d = null;
        }
    }
}
