using System.Text;

namespace PizzeriaAppTest.Utilities
{
    public static class FileOperations
    {
        static readonly string AbsolutePath = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string postFileName = "-Pizzeria";
        public const string ProductFileConst = "Products";
        public const string IngredientFileConst = "Ingredients";
        public const string OrdersConst = "Orders";
        public static string GetAbsoluteDataPath() => Path.Combine(AbsolutePath, "Data");
        public static void ValidateDataIfNotExist()
        {
            Directory.CreateDirectory(GetAbsoluteDataPath());
        }
        public static bool IsFileExist(string fileName) => File.Exists($"{GetAbsoluteDataPath()}\\{fileName}{postFileName}.json");
        public static string FilePath(string fileName) => $"{GetAbsoluteDataPath()}\\{fileName}{postFileName}.json";
        public static void InsertToJsonFile(string fileName, string? stringData = null)
        {
            ValidateDataIfNotExist();
            string filePath = FilePath(fileName);
            if (!System.IO.File.Exists(filePath))
            {
                using var file = System.IO.File.Create(filePath);
                byte[] info = new UTF8Encoding(true).GetBytes(string.IsNullOrWhiteSpace(stringData) ? "[]" : stringData); // default empty JSON array
                file.Write(info, 0, info.Length);
            }
        }
        public static string ReadFromJsonFile(string fileName)
        {
            string filePath = FilePath(fileName);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }
        }
        public static void WriteToJsonFile(string fileName, string jsonData)
        {
            string filePath = FilePath(fileName);
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, jsonData); // Overwrite existing data
            }
            else
            {
                InsertToJsonFile(fileName, jsonData); // Create new file with provided data
            }
        }
        public static void AppendToJsonFile(string fileName, string jsonData)
        {
            string filePath = FilePath(fileName);
            if (File.Exists(filePath))
            {
                var existingData = File.ReadAllText(filePath);
                var updatedData = existingData.TrimEnd(']').Trim() + (existingData.Trim() == "[]" ? "" : ",") + jsonData.TrimStart('[').Trim();
                File.WriteAllText(filePath, updatedData);// Write updated data back to the file
            }
            else
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }
        }
    }
}
