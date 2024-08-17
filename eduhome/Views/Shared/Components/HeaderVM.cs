namespace BackEndProject_Edu.Views.Shared.Components
{
    public class HeaderVM
    {
        public int BasketCount { get; set; }
        public decimal TotalPrice { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
