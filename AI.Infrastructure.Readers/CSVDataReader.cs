using AI.Infrastructure.Readers.Interfaces;
using System;
using AI.Infrastructure.Data.Dtos;
using System.Collections.Generic;
using AI.Infrastructure.Connectors.Interfaces;

namespace AI.Infrastructure.Readers 
{
    public class CSVDataReader : IDataRead<IList<DToTypeExample1>> // Whats within the Brackets is the output type
    {
        private readonly IDataConnection<IList<FileHelperDto>> connection;
        public CSVDataReader(IDataConnection<IList<FileHelperDto>> connection)
        {
            this.connection = connection;
        }
        public IList<DToTypeExample1> LoadData()
        {
            var loadedCSVData = connection.LoadData();
            foreach(var row in loadedCSVData)
            {
                // load this data into another Dto, more defined and less generic
            }
            throw new NotImplementedException(); // we will get to exceptions shortly and extensions thereof
        }
    }
}
