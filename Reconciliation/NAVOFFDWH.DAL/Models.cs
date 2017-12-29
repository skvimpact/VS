using System;
using System.Data.Common;
using System.Collections.Generic;
using Script_Executor;
using Bulk_Inserter;
using System.Collections;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NAVOFFDWH_DAL
{

    public class StringIntMap : IEnumerable
    {
        private IDictionary<string, int> _dictionary;

        public StringIntMap(IDictionary<string, int> dictionary)
        {
            _dictionary = dictionary;
        }

        public bool ContainsKey(string BKey) => _dictionary.ContainsKey(BKey);

        public int MaxInt => _dictionary.Count == 0 ? 0 : _dictionary.Values.Max();

        public int this[string BKey]
        {
            get => _dictionary[BKey];
            set => _dictionary[BKey] = value;
        }

        public void ClearMap() => _dictionary.Clear();

        public int Count => _dictionary.Count;

        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
    }
}
