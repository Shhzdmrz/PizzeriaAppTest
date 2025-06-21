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
                            new Ingredient { Name = "Dough", Amount = 1 },
                            new Ingredient { Name = "Tomato Sauce", Amount =0.5 },
                            new Ingredient { Name = "Mozzarella", Amount = 0.8 },
                            new Ingredient { Name = "Basil", Amount = 0.1 },
                            new Ingredient { Name = "BBQ Sauce", Amount = 0.5 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 2,
                        Ingredients = [
                            new Ingredient { Name = "Dough", Amount =1 },
                            new Ingredient{ Name = "Tomato Sauce", Amount =0.5 },
                            new Ingredient{ Name = "Mozzarella", Amount =0.8 },
                            new Ingredient{ Name = "Pepperoni", Amount =1 },
                            new Ingredient { Name = "Onions", Amount = 0.3 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 3,
                        Ingredients = [
                            new Ingredient{Name =  "Dough", Amount =1 },
                            new Ingredient{ Name = "Tomato Sauce", Amount =0.5 },
                            new Ingredient{Name =  "Mozzarella", Amount =0.8 },
                            new Ingredient{ Name = "Bell Peppers", Amount =0.3 },
                            new Ingredient{Name =  "Olives", Amount =0.2 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 4,
                        Ingredients = [
                            new Ingredient{ Name = "Dough", Amount =1 },
                            new Ingredient{Name =  "BBQ Sauce", Amount =0.5 },
                            new Ingredient{ Name = "Mozzarella", Amount =0.8 },
                            new Ingredient{ Name = "Chicken", Amount =1.2 },
                            new Ingredient{ Name = "Onions", Amount =0.3 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 5,
                        Ingredients = [
                            new Ingredient{ Name = "Dough", Amount =1 },
                            new Ingredient{ Name = "Olives",Amount = 0.5 },
                            new Ingredient{ Name = "Mozzarella",Amount = 0.8 },
                            new Ingredient{ Name = "Chicken",Amount = 1.2 },
                            new Ingredient{ Name = "Onions", Amount =0.3 }
                        ]
                    }
                ];
        }
        public static bool ValidateOrderProductIngredient(OrderItem orderItem)
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
        public static List<ProductIngredient> LoadProductIngredients()
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
        public static Dictionary<string, double> CalculateTotalIngredients(List<OrderItem> orderItems, List<ProductIngredient> ingredientsList)
        {
            var totalIngredients = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

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

            return totalIngredients;
        }
    }
}
