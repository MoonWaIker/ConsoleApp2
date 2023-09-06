namespace ConsoleApp2.Library
{
    internal interface IReader
    {
        public string FileExtension { get; }

        public Car[] Read(string filePath);

        public void Write(string filePath, Car[] items);

        public void Add(string filePath, Car[] items);

        public void Edit(string filePath, Car[] items);

        public void Delete(string filePath, string[] items);
    }
}
