namespace WebAPI.Controllers
{
    using DAL;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;

    [RoutePrefix("api/fruits")]
    public class FruitsController : ApiController
    {
        private FruitsContext _context;

        public FruitsController()
        {
            this._context = new FruitsContext();
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetFruits()
        {
            List<Fruit> fruits = this._context.Fruits.ToList();
            return Ok(fruits);
        }

        [HttpGet, Route("{id}")]
        public IHttpActionResult GetFruitById(int id)
        {
            Fruit fruit = this._context.Fruits.Find(id);

            if (fruit != null)
            {
                return Ok(fruit);
            }

            return NotFound();
        }

        [HttpPut, Route("")]
        public IHttpActionResult NewFruit(Fruit fruit)
        {
            this._context.Entry(fruit).State = EntityState.Added;
            int saved = this._context.SaveChanges();

            if (saved > 0)
            {
                return Ok(fruit.Id);
            }

            return BadRequest();
        }

        [HttpPatch, Route("{id}")]
        public IHttpActionResult UpdateFruit(int id, Fruit fruit)
        {
            Fruit existingFruit = this._context.Fruits.Find(id);

            if (existingFruit == null)
            {
                return NotFound();
            }

            fruit.Id = id;
            this._context.Entry(fruit).State = EntityState.Modified;
            int saved = this._context.SaveChanges();

            if (saved > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete, Route("{id}")]
        public IHttpActionResult DeleteFruit(int id)
        {
            Fruit fruitToDelete = this._context.Fruits.Find(id);

            if (fruitToDelete == null)
            {
                return NotFound();
            }

            this._context.Entry(fruitToDelete).State = EntityState.Deleted;
            int saved = this._context.SaveChanges();

            if (saved > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
            this._context = null;
            base.Dispose(disposing);
        }
    }
}