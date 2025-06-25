using Newtonsoft.Json;
using PizzeriaAppTest.Utilities;
using PizzeriaAppTest.Models;

namespace PizzeriaAppTest.Data
{
    public static class SeedData
    {
        public static void Load()
        {
            Console.WriteLine("Initializing data files...");
            if (!FileOperations.IsFileExist(FileOperations.ProductFileConst))
            {
                try
                {
                    Console.WriteLine("Seeding products data for app...");
                    var products = Product.LoadSeed();
                    var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
                    FileOperations.InsertToJsonFile(FileOperations.ProductFileConst, productsJson);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding product data to file: {ex.Message}");
                    throw;
                }
            }
            if (!FileOperations.IsFileExist(FileOperations.IngredientFileConst))
            {
                try
                {
                    Console.WriteLine("Seeding ingredients for product data of app...");
                    var ingredients = ProductIngredient.LoadSeed();
                    var ingredientsJson = JsonConvert.SerializeObject(ingredients, Formatting.Indented);
                    FileOperations.InsertToJsonFile(FileOperations.IngredientFileConst, ingredientsJson);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding ingredients data to file: {ex.Message}");
                    throw;
                }
            }
            Console.WriteLine("Data files initialized successfully!");
        }
    }
}
