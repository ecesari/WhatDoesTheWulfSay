namespace EverybodyCodes.Infrastructure.Csv
{
    public interface ICsvReader<T>
    {
        public IEnumerable<T> Read(Stream stream);
    }
}
