using AI.Infrastructure.Connectors.Interfaces;
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.Infrastructure.Connectors
{
    public class CsvDataWriterConnection<T> : IDataWriteConnection<T> where T : class
    {
        private readonly FileHelperEngine<T> engine;
        private string headerText;
        public CsvDataWriterConnection(string headerText)
        {
            engine = new FileHelperEngine<T>(Encoding.UTF8);
            this.headerText = headerText;
        }

        public virtual void WriteData(IList<T> values, string path, string delimiter)
        {
            //engine.HeaderText = headerText;
            engine.WriteFile(path, values);
        }
    }
}
