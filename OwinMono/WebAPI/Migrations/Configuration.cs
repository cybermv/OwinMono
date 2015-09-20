namespace WebAPI.Migrations
{
    using DAL;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GroceriesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}