namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.BlogVMs
{
    public class BlogUpdateVM
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string Desc { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
    }
}

