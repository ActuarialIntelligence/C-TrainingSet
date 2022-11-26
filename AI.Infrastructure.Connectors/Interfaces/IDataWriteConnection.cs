using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Infrastructure.Connectors.Interfaces
{

    public interface IDataWriteConnection<T>
    {
        void WriteData(IList<T> values, string path, string delimiter);
    }
}
