using System;
using System.Data.Entity;

namespace OpenDataFx
{
    public interface IDbFactory : IDisposable
    {
        DbContext GetDataContext<T>() where T : DbContext, new();
    }
}
