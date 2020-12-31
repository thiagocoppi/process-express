using System;
using System.Data;

namespace Infraestrutura.Context
{
    public interface IProcessExpressContext : IDisposable
    {
        IDbConnection GetConnection();
    }
}