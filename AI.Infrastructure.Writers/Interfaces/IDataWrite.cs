

using System.Collections.Generic;

namespace AI.Infrastructure.Writers.Interfaces
{
    public interface IDataWrite<T>
    {
        void WriteData(IList<T> data);
    }
}
