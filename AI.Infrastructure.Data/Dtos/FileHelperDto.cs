
    using FileHelpers;
namespace AI.Infrastructure.Data.Dtos
{

        [DelimitedRecord(",")] // Tells us the delimiter
        [IgnoreFirst]
        public class FileHelperDto // defines the return object with all of the rows that will be populated
        {
            public string[] Row;
        }
    
}
