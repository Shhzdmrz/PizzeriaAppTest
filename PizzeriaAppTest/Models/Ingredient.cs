namespace PizzeriaAppTest.Models
{
    public class Ingredient
    {
        public string Name { get; set; } = string.Empty;
        public double Quantity { get; set; }
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
                            new Ingredient { Name = "Dough", Quantity = 1 },
                            new Ingredient { Name = "Tomato Sauce", Quantity =0.5 },
                            new Ingredient { Name = "Mozzarella", Quantity = 0.8 },
                            new Ingredient { Name = "Basil", Quantity = 0.1 },
                            new Ingredient { Name = "BBQ Sauce", Quantity = 0.5 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 2,
                        Ingredients = [
                            new Ingredient { Name = "Dough", Quantity =1 },
                            new Ingredient{ Name = "Tomato Sauce", Quantity =0.5 },
                            new Ingredient{ Name = "Mozzarella", Quantity =0.8 },
                            new Ingredient{ Name = "Pepperoni", Quantity =1 },
                            new Ingredient { Name = "Onions", Quantity = 0.3 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 3,
                        Ingredients = [
                            new Ingredient{Name =  "Dough", Quantity =1 },
                            new Ingredient{ Name = "Tomato Sauce", Quantity =0.5 },
                            new Ingredient{Name =  "Mozzarella", Quantity =0.8 },
                            new Ingredient{ Name = "Bell Peppers", Quantity =0.3 },
                            new Ingredient{Name =  "Olives", Quantity =0.2 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 4,
                        Ingredients = [
                            new Ingredient{ Name = "Dough", Quantity =1 },
                            new Ingredient{Name =  "BBQ Sauce", Quantity =0.5 },
                            new Ingredient{ Name = "Mozzarella", Quantity =0.8 },
                            new Ingredient{ Name = "Chicken", Quantity =1.2 },
                            new Ingredient{ Name = "Onions", Quantity =0.3 }
                        ]
                    },
                    new ProductIngredient {
                        ProductId = 5,
                        Ingredients = [
                            new Ingredient{ Name = "Dough", Quantity =1 },
                            new Ingredient{ Name = "Olives",Quantity = 0.5 },
                            new Ingredient{ Name = "Mozzarella",Quantity = 0.8 },
                            new Ingredient{ Name = "Chicken",Quantity = 1.2 },
                            new Ingredient{ Name = "Onions", Quantity =0.3 }
                        ]
                    }
                ];
        }
    }  
}
