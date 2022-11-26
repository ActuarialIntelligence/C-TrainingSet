using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Infrastructure.Readers.Interfaces
{
    public interface IDataRead<T> // Readers will be different, but however will behave in some same way.
    {
        T LoadData(); // T is the generic output type and this is how the object will behave if inherited
        // T will be specified upon instantiating the reader object, we will see how.
    }
}
