using Newtonsoft.Json;
using PizzeriaAppTest.Utilities;

namespace PizzeriaAppTest.Models
{
    public class Ingredient
    {
        public string Name { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public Ingredient[] Ingredients { get; set; } = [];
        public static ProductIngredient[] LoadSeed()
        {
            return
                [
                    new ProductIngredient {
                        ProductId = 1,
                        Ingredients = [
                            new() { Name = "Dough (g)", Amount = 250 },
                            new() { Name = "Tomato Sauce (ml)", Amount = 100 },
                            new() { Name = "Mozzarella (g)", Amount = 150 },
                            new() { Name = "Basil (g)", Amount = 5 },
                            new() { Name = "BBQ Sauce (ml)", Amount = 80 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 2,
                        Ingredients = [
                            new() { Name = "Dough (g)", Amount = 250 },
                            new() { Name = "Tomato Sauce (ml)", Amount = 100 },
                            new() { Name = "Mozzarella (g)", Amount = 150 },
                            new() { Name = "Pepperoni (g)", Amount = 80 },
                            new() { Name = "Onions (g)", Amount = 40 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 3,
                        Ingredients = [
                            new() { Name =  "Dough (g)", Amount = 250 },
                            new() { Name = "Tomato Sauce (ml)", Amount = 100 },
                            new() { Name =  "Mozzarella (g)", Amount = 150 },
                            new() { Name = "Bell Peppers (g)", Amount = 50 },
                            new() { Name =  "Olives (g)", Amount = 30 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 4,
                        Ingredients = [
                            new() { Name = "Dough (g)", Amount = 250 },
                            new() { Name =  "BBQ Sauce (ml)", Amount = 80 },
                            new() { Name = "Mozzarella (g)", Amount = 150 },
                            new() { Name = "Chicken (g)", Amount = 120 },
                            new() { Name = "Onions (g)", Amount = 40 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 5,
                        Ingredients = [
                            new() { Name = "Dough (g)", Amount = 250 },
                            new() { Name = "Tomato Sauce (ml)", Amount = 100 },
                            new() { Name = "Mozzarella (g)",Amount = 150 },
                            new() { Name = "Chicken (g)",Amount = 120 },
                            new() { Name = "Onions (g)", Amount = 40 }
                        ]
                    }
                ];
        }
        public static bool ValidateOrderProductIngredient(OrderItem orderItem)
        {
            try
            {
                var ingredientsFile = FileOperations.FilePath(FileOperations.IngredientFileConst);
                if (!FileOperations.IsFileExist(FileOperations.IngredientFileConst))
                {
                    return false;
                }
                var ingredientData = File.ReadAllText(ingredientsFile);
                if (string.IsNullOrWhiteSpace(ingredientData))
                {
                    return false;
                }
                var ingredientList = JsonConvert.DeserializeObject<List<ProductIngredient>>(ingredientData);
                if (ingredientList == null || !ingredientList.Any())
                {
                    return false;
                }
                if (!ingredientList.Any(p => p.ProductId == orderItem.ProductId))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while validating order product from ingredients: {ex.Message}");
                return false;
            }
        }
        public static List<ProductIngredient> LoadProductIngredients()
        {
            try
            {
                var ingredientFile = FileOperations.FilePath(FileOperations.IngredientFileConst);
                if (!FileOperations.IsFileExist(FileOperations.IngredientFileConst))
                {
                    return new();
                }
                var ingredientString = File.ReadAllText(ingredientFile);
                if (string.IsNullOrWhiteSpace(ingredientString))
                {
                    return new();
                }
                var productIngredients = JsonConvert.DeserializeObject<List<ProductIngredient>>(ingredientString);
                return productIngredients ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading ingredients: {ex.Message}");
                return new();
            }
        }
        public static Dictionary<string, double> CalculateTotalIngredients(List<OrderItem> orderItems, List<ProductIngredient> ingredientsList)
        {
            var totalIngredients = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            try
            {
                foreach (var orderItem in orderItems)
                {
                    var productIngredients = ingredientsList.FirstOrDefault(p => p.ProductId == orderItem.ProductId);

                    if (productIngredients == null || productIngredients.Ingredients == null)
                        continue;

                    foreach (var ingredient in productIngredients.Ingredients)
                    {
                        double requiredAmount = Math.Round(ingredient.Amount * orderItem.Quantity, 2);// Multiply each ingredient's amount by the quantity ordered

                        if (totalIngredients.ContainsKey(ingredient.Name))
                        {
                            totalIngredients[ingredient.Name] += requiredAmount;
                        }
                        else
                        {
                            totalIngredients[ingredient.Name] = requiredAmount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating total ingredients: {ex.Message}");
            }
            return totalIngredients;
        }
    }
}
