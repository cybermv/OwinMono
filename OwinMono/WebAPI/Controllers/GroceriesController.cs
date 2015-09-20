namespace WebAPI.Controllers
{
    using DAL.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    [RoutePrefix("api/groceries")]
    public class GroceriesController : ControllerBase
    {
        // TODO IHAR
        [Route("")]
        public List<Grocery> GetGroceries()
        {
            return this.GroceriesContext.Groceries.ToList();
        }

        [Route("{id}")]
        public Grocery GetGroceryById(int id)
        {
            return this.GroceriesContext.Groceries.Find(id);
        }

        [Route("{id}/fruits")]
        public List<Fruit> GetGroceryFruits(int id)
        {
            return this.GroceriesContext.Fruits.Where(f => f.Grocer.Id == id).ToList();
        }
    }
}