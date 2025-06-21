using Newtonsoft.Json;
using PizzeriaAppTest.Utilities;
using PizzeriaAppTest.Models;

namespace PizzeriaAppTest.Data
{
    public class SeedData
    {
        public static void Load()
        {
            if (!File.Exists(FileOperations.GetAbsoulteDataPath() + FileOperations.ProductFileConst))
            {
                Console.WriteLine("Seeding products data for app...");
                var products = Product.LoadSeed();
                var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
                FileOperations.InsertToJsonFile(FileOperations.ProductFileConst, productsJson);
            }
            if (!File.Exists(FileOperations.GetAbsoulteDataPath() + FileOperations.IngredientFileConst))
            {
                Console.WriteLine("Seeding ingredients for product data of app...");
                var ingredients = ProductIngredient.LoadSeed();
                var ingredientsJson = JsonConvert.SerializeObject(ingredients, Formatting.Indented);
                FileOperations.InsertToJsonFile(FileOperations.IngredientFileConst, ingredientsJson);
            }
            Console.WriteLine("Application started!...");
        }
    }
}
