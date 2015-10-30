using System;


namespace Daisy.Dal.Context.Interfaces
{
    public interface IConnectionFactory
    {
        IConnection CreateDapperConnection();
    }
}
