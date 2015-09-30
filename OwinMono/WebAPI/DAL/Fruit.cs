namespace WebAPI.DAL
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum FruitColor
    {
        Green = 1,
        Yellow = 2,
        Red = 3,
        Orange = 4,
        Blue = 5
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