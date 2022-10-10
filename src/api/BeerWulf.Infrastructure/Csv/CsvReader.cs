using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace EverybodyCodes.Infrastructure.Csv
{
    public class CsvReader<T> : ICsvReader<T>
    {
        
        public IEnumerable<T> Read(Stream stream)
        {

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";",
            };

            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, configuration);
            List<T>? records = new List<T>();


            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<T>();
                    records.Add(record);
                }
                catch (CsvHelperException e)
                {
                    Console.WriteLine($"{e.Message} at row:" + csv.Parser.RawRow + (e.InnerException == null ? string.Empty : e.InnerException.Message));
                    //logger.LogError("Csv row not converted", e);
                }
            }


            return records;

        }
    }
}
