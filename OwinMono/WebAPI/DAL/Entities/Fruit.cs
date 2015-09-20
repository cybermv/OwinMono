namespace WebAPI.DAL.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public enum FruitColor
    {
        Green,
        Yellow,
        Red,
        Orange,
        Blue
    }

    [Table("Fruits")]
    public class Fruit : EntityBase
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public FruitColor Color { get; set; }

        public virtual Grocery Grocer { get; set; }
    }
}