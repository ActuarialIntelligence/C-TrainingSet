using AI.Infrastructure.Data.Dtos;
using AI.Infrastructure.Readers;
using AI.Infrastructure.Readers.Interfaces;
using System;
using AI.Infrastructure.Connectors;
using System.Collections.Generic;

namespace AI.ServiceLocator
{
    // The service locator pattern is an IoC pattern that centralises the 
    // interface associations and creation of the objects we make use of in the solution.
    // There are other ways of achieving this via other libraries like Ninject and NHibernate....
    // we will cover that at a later point as they only minimize your code but adds overhead
    public static class DependencyResolution
    {
        public static IDataRead<IList<DToTypeExample1>> ConstructAndReturnCsvWriter()
        {
            var connection = new CsvDataConnector<FileHelperDto>(""); // obtain from App.Config in play
            var reader = new CSVDataReader(connection);
            return reader; // This reader must completely be destroyed at the calling end 
                            // or the potential for memory leak is realised.
         }
    }
}
