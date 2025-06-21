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
        public static void InsertToJsonFile(string fileName, string? stringData = null)
        {
            ValidateDataIfNotExist();
            string filePath = Path.Combine(GetAbsoulteDataPath(), $"{fileName}{postFileName}.json");
            if (!System.IO.File.Exists(filePath))
            {
                using var file = System.IO.File.Create(filePath);
                byte[] info = new UTF8Encoding(true).GetBytes(string.IsNullOrWhiteSpace(stringData) ? "[]" : stringData); // default empty JSON array
                file.Write(info, 0, info.Length);
            }
        }
        public static void AppendToJsonFile(string fileName, string jsonData)
        {
            string filePath = Path.Combine(AbsolutePath, $"{fileName}{postFileName}.json");
            if (System.IO.File.Exists(filePath))
            {
                // Read existing data
                var existingData = System.IO.File.ReadAllText(filePath);
                // Append new data to the existing data
                var updatedData = existingData.TrimEnd(']') + "," + jsonData.TrimStart('[') + "]";
                // Write updated data back to the file
                System.IO.File.WriteAllText(filePath, updatedData);
            }
            else
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }
        }
    }
}
