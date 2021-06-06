using System.Data.Entity;
using Lab1.Models.Entities;

namespace Lab1.Models
{
    public class TouristAgencyDbEntities : DbContext
    {
        /*
        public TouristAgencyDbEntities() : base ("name=TouristAgencyDbEntities") { }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        } 
        */

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourOperator> TourOperators { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
    }
}