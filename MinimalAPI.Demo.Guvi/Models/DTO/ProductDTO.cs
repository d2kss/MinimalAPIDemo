namespace MinimalAPI.Demo.Guvi.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActvie { get; set; }

        public DateTime? Created { get; set; }
    }
}
