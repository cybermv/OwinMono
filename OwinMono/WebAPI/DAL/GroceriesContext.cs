namespace WebAPI.DAL
{
    using Common;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    public class GroceriesContext : DbContext
    {
        public GroceriesContext()
            : base(nameOrConnectionString: "name=GroceriesContext")
        {
            this.Database.Log = message =>
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    ConsoleLogger.Instance.Write(message, ConsoleColor.Blue);
                }
            };

            this.Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new GroceriesContextInitializer());
        }

        public virtual DbSet<Grocery> Groceries { get; set; }

        public virtual DbSet<Fruit> Fruits { get; set; }
    }

    public class GroceriesContextInitializer : CreateDatabaseIfNotExists<GroceriesContext>
    {
        protected override void Seed(GroceriesContext context)
        {
            context.Groceries.AddOrUpdate(
                grocery => grocery.Name,
                new Grocery
                {
                    Name = "Mr.Frank's fruits",
                    Address = "Fulton road 432",
                    AvailableFruits = new List<Fruit>
                    {
                        new Fruit {Name = "Apple", Color = FruitColor.Red, Price = 4.7m},
                        new Fruit {Name = "Blueberry", Color = FruitColor.Blue, Price = 7.55m},
                        new Fruit {Name = "Pear", Color = FruitColor.Green, Price = 5m}
                    }
                },
                new Grocery
                {
                    Name = "Timmy's lemonade stand",
                    Address = "Leafy hollow 85",
                    AvailableFruits = new List<Fruit>
                    {
                        new Fruit {Name = "Lemon", Color = FruitColor.Yellow, Price = 13.42m}
                    }
                },
                new Grocery
                {
                    Name = "SuperFruits market",
                    Address = "Industrious road 1175",
                    AvailableFruits = new List<Fruit>
                    {
                        new Fruit {Name = "Pineapple", Color = FruitColor.Yellow, Price = 25.99m},
                        new Fruit {Name = "Orange", Color = FruitColor.Orange, Price = 6.72m},
                        new Fruit {Name = "Grapes", Color = FruitColor.Blue, Price = 7.88m},
                        new Fruit {Name = "Tomato", Color = FruitColor.Red, Price = 2.6m},
                        new Fruit {Name = "Lime", Color = FruitColor.Green, Price = 14.44m},
                    }
                });
        }
    }
}