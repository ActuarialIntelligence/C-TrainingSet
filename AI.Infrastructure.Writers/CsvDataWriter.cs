using AI.Infrastructure.Connectors.Interfaces;
using AI.Infrastructure.Data.Dtos;
using AI.Infrastructure.Writers.Interfaces;
using System;
using System.Collections.Generic;

namespace AI.Infrastructure.Writers
{
    public class CsvDataWriter : IDataWrite<WriteDto>
    {
        private readonly IDataWriteConnection<WriteDto> connection;
        public CsvDataWriter(IDataWriteConnection<WriteDto> connection) // Pass the connection through the constructer for effective IOC
        {
            this.connection = connection;
        }
        public void WriteData(IList<WriteDto> data)
        {
            connection.WriteData(data,"",","); // As for teh PATH, we will get to App.Config in our next section
            //throw new NotImplementedException();
        }
    }
}
