using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1.Library
{
    internal class JsonReader : IReader
    {
        public string FileExtension { get; } = ".json";

        public void Add(string filePath, Car[] items)
        {
            DepencyInjection(filePath);

            var documentObject = new { Document = items.Concat(Read(filePath)) };
            string json = JsonConvert.SerializeObject(documentObject);
            File.WriteAllText(filePath, json);
        }

        public void Delete(string filePath, string[] items)
        {
            DepencyInjection(filePath);

            var documentObject = new
            {
                Document = Read(filePath)
                .Where(item => !items.Contains(item.BrandName))
            };
            string json = JsonConvert.SerializeObject(documentObject);
            File.WriteAllText(filePath, json);
        }

        public void Edit(string filePath, Car[] items)
        {
            DepencyInjection(filePath);

            var list = items
                .Select(item => item.BrandName);
            var documentObject = new
            {
                Document = Read(filePath)
                .Where(item => !list.Contains(item.BrandName))
                .Concat(items)
            };
            string json = JsonConvert.SerializeObject(documentObject);
            File.WriteAllText(filePath, json);
        }

        public Car[] Read(string filePath)
        {
            DepencyInjection(filePath);

            string jsonString = File.ReadAllText(filePath);
            return (JObject.Parse(jsonString)["Document"] ?? string.Empty)
            .Select(car => car.ToObject<Car>() ?? throw new Exception())
            .ToArray();
        }

        public void Write(string filePath, Car[] items)
        {
            DepencyInjection(filePath);

            var documentObject = new { Document = items };
            string json = JsonConvert.SerializeObject(documentObject);
            File.WriteAllText(filePath, json);
        }

        private void DepencyInjection(string filePath)
        {
            if (filePath.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            if (Path.GetExtension(filePath) != FileExtension)
            {
                throw new ArgumentException("File must be 'json' extension", nameof(filePath));
            }
        }
    }
}
