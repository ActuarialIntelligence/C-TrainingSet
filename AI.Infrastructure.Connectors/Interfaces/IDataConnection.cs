using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Infrastructure.Connectors.Interfaces
{
    public interface IDataConnection<T>
    {
        T LoadData();
    }
}
