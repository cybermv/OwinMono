namespace WebAPI.DAL
{
    using Common;
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class FruitsContext : DbContext
    {
        public FruitsContext()
            : base(nameOrConnectionString: "name=FruitsContext")
        {
            this.Database.Log = message =>
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    ConsoleLogger.Instance.Write(message, ConsoleColor.Blue);
                }
            };

            this.Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new FruitsContextInitializer());
        }

        public virtual DbSet<Fruit> Fruits { get; set; }
    }

    public class FruitsContextInitializer : CreateDatabaseIfNotExists<FruitsContext>
    {
        protected override void Seed(FruitsContext context)
        {
            context.Fruits.AddOrUpdate(
                f => f.Name,
                new Fruit { Name = "Apple", Color = FruitColor.Red, Price = 4.7m },
                new Fruit { Name = "Blueberry", Color = FruitColor.Blue, Price = 7.55m },
                new Fruit { Name = "Pear", Color = FruitColor.Green, Price = 5m },
                new Fruit { Name = "Lemon", Color = FruitColor.Yellow, Price = 13.42m },
                new Fruit { Name = "Pineapple", Color = FruitColor.Yellow, Price = 25.99m },
                new Fruit { Name = "Orange", Color = FruitColor.Orange, Price = 6.72m },
                new Fruit { Name = "Grapes", Color = FruitColor.Blue, Price = 7.88m },
                new Fruit { Name = "Tomato", Color = FruitColor.Red, Price = 2.6m },
                new Fruit { Name = "Lime", Color = FruitColor.Green, Price = 14.44m },
                new Fruit { Name = "Banana", Color = FruitColor.Yellow, Price = 11.89m }
                );

            context.SaveChanges();
        }
    }
}