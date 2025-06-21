using System.Text;

namespace PizzeriaAppTest.Utilities
{
    public static class FileOperations
    {
        static string AbsolutePath = AppDomain.CurrentDomain.BaseDirectory;
        static string postFileName = "-Pizzeria";
        public static string ProductFileConst = "Products";
        public static string IngredientFileConst = "Ingredients";
        public static string OrdersConst = "Orders";
        public static string GetAbsoulteDataPath() => Path.Combine(AbsolutePath, "Data");
        public static void ValidateDataIfNotExist()
        {
            Directory.CreateDirectory(GetAbsoulteDataPath());
        }
        public static bool IsFileExist(string fileName) => File.Exists($"{GetAbsoulteDataPath()}\\{fileName}{postFileName}.json");
        public static string FilePath(string fileName) => $"{GetAbsoulteDataPath()}\\{fileName}{postFileName}.json";
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
