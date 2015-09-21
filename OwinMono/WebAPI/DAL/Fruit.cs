namespace WebAPI.DAL
{
    using System.ComponentModel.DataAnnotations;
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
    public class Fruit
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public FruitColor Color { get; set; }
    }
}