using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;
namespace AI.Infrastructure.Data.Dtos
{
    [DelimitedRecord(",")] // Tells us the delimiter
    public class WriteDto
    {
        public int id;
        public string name;
        public string description;
        public DateTime dateTime;
    }
}
