namespace WebAPI.Controllers
{
    using DAL;
    using System.Web.Http;

    public abstract class ControllerBase : ApiController
    {
        private GroceriesContext _dbContext;

        protected ControllerBase()
        {
            this._dbContext = new GroceriesContext();
        }

        public GroceriesContext GroceriesContext { get { return this._dbContext; } }

        protected override void Dispose(bool disposing)
        {
            this._dbContext.Dispose();
            this._dbContext = null;
            base.Dispose(disposing);
        }
    }
}