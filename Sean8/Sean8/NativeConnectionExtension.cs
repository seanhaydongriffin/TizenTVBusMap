using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sean8
{
    public static class NativeConnectionExtension
    {
        public static List<T> SelectAllFrom<T>(this SQLiteConnection cnn) where T : new()
        {
            var mapping = cnn.GetMapping<T>();
            var result = cnn.Query<T>(String.Format("select * from {0};", mapping.TableName));
            return result;
        }
        public static List<T> SelectFrom<T>(this SQLiteConnection cnn, string query) where T : new()
        {
            var mapping = cnn.GetMapping<T>();
            var result = cnn.Query<T>(String.Format(query, mapping.TableName));
            return result;
        }
    }
}
