namespace BookRental.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BookRentalContext : DbContext
    {
        // Your context has been configured to use a 'BookRentalContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BookRental.Models.BookRentalContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BookRentalContext' 
        // connection string in the application configuration file.
        public BookRentalContext()
            : base("name=BookRentalContext")
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookRentalContext, BookRental.Migrations.Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}