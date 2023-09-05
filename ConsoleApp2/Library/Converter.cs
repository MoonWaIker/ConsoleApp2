namespace ConsoleApp1.Library
{
    internal class Converter
    {
        public static void Convert(string filePathFrom, IReader from, IReader To, string? filePathTo = default)
        {
            Car[] cars = from.Read(filePathFrom);
            filePathTo ??= Path.GetDirectoryName(filePathFrom) + '\\' + Path.GetFileNameWithoutExtension(filePathFrom) + To.FileExtension;
            To.Write(filePathTo, cars);
        }
    }
}
