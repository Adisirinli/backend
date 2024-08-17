namespace BackEndProject_Edu.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public int BasketCount { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
