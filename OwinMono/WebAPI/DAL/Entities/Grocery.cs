namespace WebAPI.DAL.Entities
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Groceries")]
    public class Grocery : EntityBase
    {
        public Grocery()
        {
            this.AvailableFruits = new Collection<Fruit>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Fruit> AvailableFruits { get; set; }
    }
}