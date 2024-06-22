namespace MinimalAPI.Demo.Guvi
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActvie {  get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set;}
    }
}
