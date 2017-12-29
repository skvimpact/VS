using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Bulk_Inserter
{
    public interface IBulkInsertable<out T>
    {
        string DestinationTableName { get; }
        IDictionary<string, string> BulkMapper();
    }



    public class BulkInserter
    {
        
        SqlConnection _sqlConnection;

        public BulkInserter(string ConnectionString)
        {
            _sqlConnection = new SqlConnection(ConnectionString);
        }

        public void Run<T>(IEnumerable<T> set) where T : IBulkInsertable<T>, new()
        {
            T item = new T();

            IDictionary<string, string> mapping = item.BulkMapper();
            string[] fields = new string[mapping.Keys.Count];
            mapping.Keys.CopyTo(fields, 0);

            EnumerableDataReader<T> edr = new EnumerableDataReader<T>(set, fields);

            _sqlConnection.Open();

            SqlBulkCopy bulkCopy = new SqlBulkCopy(_sqlConnection)
            {
                DestinationTableName    =   item.DestinationTableName,
                BulkCopyTimeout         =   0
            };

            foreach (var key in mapping.Keys)
            {
                bulkCopy.ColumnMappings.Add(key, mapping[key]);
            }
            
            bulkCopy.WriteToServer(edr);

            _sqlConnection.Close();
        }

        public void Run<T>(IEnumerable<T> collection, IDictionary<string, string> mapper, string destinationTableName) 
        {
         //   string v = (string)typeof(T).GetMember("DestinationTableName").ToString();
            string[] fields = new string[mapper.Keys.Count];
            mapper.Keys.CopyTo(fields, 0);

            EnumerableDataReader<T> edr = new EnumerableDataReader<T>(collection, fields);

            _sqlConnection.Open();

            SqlBulkCopy bulkCopy = new SqlBulkCopy(_sqlConnection)
            {
                DestinationTableName = destinationTableName,// (string)typeof(T).GetMethod("DestinationTableName").Invoke(null, new object[]{ }),
                BulkCopyTimeout = 0
            };

            foreach (var key in mapper.Keys)
            {
                bulkCopy.ColumnMappings.Add(key, mapper[key]);
            }

            bulkCopy.WriteToServer(edr);

            _sqlConnection.Close();
        }


    }
}
