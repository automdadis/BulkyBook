using BulkyBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//Προγραμμα για σύνδεση της db
namespace BulkyBook.DataAccess;

public class ApplicationDbContext: IdentityDbContext 

{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    //Αυτη η γραμμη συνδέεται με το Model το οποίο φτιάξαμε
    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<Product> Products { get; set; }
}
