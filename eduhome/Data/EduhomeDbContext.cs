
using BackEndProject_Edu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.ContentModel;
using NuGet.DependencyResolver;
using System.Reflection.Metadata;

namespace eduhome.Data
{
    public class EduhomeDbContext:DbContext
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Features> Features { get; set; }

        public DbSet<CourseFeature> CourseFeatures { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketCourse> BasketCOurses { get; set; }


        public DbSet<Comment> Comments { get; set; }



        public EduhomeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
