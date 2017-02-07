
using System.Collections.Generic;
using System.Data.Entity;
namespace OpenDataFx
{
    public class DbFactory : Disposable, IDbFactory
    {
        readonly Dictionary<string, DbContext> _dictionary = new Dictionary<string, DbContext>();

        public DbContext GetDataContext<T>()
            where T : DbContext, new()
        {
            var typeName = typeof(T).GetType().FullName;
            if (_dictionary.ContainsKey(typeName))
                return _dictionary[typeName];
            
            var typeValue = new T();
            _dictionary[typeName] = typeValue;
            return typeValue;
        }

        protected override void DisposeCore()
        {
            foreach (var val in _dictionary.Values)
            {
                val.Dispose();
            }
        }
    }
}
