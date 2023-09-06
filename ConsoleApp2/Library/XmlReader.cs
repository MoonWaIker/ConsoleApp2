using System.Xml.Serialization;

namespace ConsoleApp2.Library
{
    internal class XmlReader : IReader
    {
        public string FileExtension { get; } = ".xml";

        public void Add(string filePath, Car[] items)
        {
            DepencyInjection(filePath);

            Car[] read = Read(filePath);
            XmlSerializer serializer = new(typeof(Car[]), new XmlRootAttribute("Document"));
            using FileStream reader = new(filePath, FileMode.Truncate);
            serializer.Serialize(reader, items.Concat(read)
                .ToArray());
        }

        public void Delete(string filePath, string[] items)
        {
            DepencyInjection(filePath);

            Car[] read = Read(filePath);
            XmlSerializer serializer = new(typeof(Car[]), new XmlRootAttribute("Document"));
            using FileStream reader = new(filePath, FileMode.Truncate);
            serializer.Serialize(reader, read
                .Where(item => !items.Contains(item.BrandName))
                .ToArray());
        }

        public void Edit(string filePath, Car[] items)
        {
            DepencyInjection(filePath);

            Car[] read = Read(filePath);
            IEnumerable<string> list = items
                .Select(item => item.BrandName);
            XmlSerializer serializer = new(typeof(Car[]), new XmlRootAttribute("Document"));
            using FileStream reader = new(filePath, FileMode.OpenOrCreate);
            serializer.Serialize(reader, read
                .Where(item => !list.Contains(item.BrandName))
                .Concat(items)
                .ToArray());
        }

        public Car[] Read(string filePath)
        {
            DepencyInjection(filePath);

            XmlSerializer serializer = new(typeof(Car[]), new XmlRootAttribute("Document"));
            using FileStream reader = new(filePath, FileMode.Open);
            return serializer.Deserialize(reader) as Car[] ?? Array.Empty<Car>();
        }

        public void Write(string filePath, Car[] items)
        {
            DepencyInjection(filePath);

            XmlSerializer serializer = new(typeof(Car[]), new XmlRootAttribute("Document"));
            using FileStream reader = new(filePath, FileMode.OpenOrCreate);
            serializer.Serialize(reader, items);
        }

        private void DepencyInjection(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            if (Path.GetExtension(filePath) != FileExtension)
            {
                throw new ArgumentException("File must be 'xml' extension", nameof(filePath));
            }
        }
    }
}
