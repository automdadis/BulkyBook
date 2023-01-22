using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
//Προγραμμα για σύνδεση της db
namespace BulkyBook.DataAccess;

public class ApplicationDbContext: DbContext 

{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    //Αυτη η γραμμη συνδέεται με το Model το οποίο φτιάξαμε
    public DbSet<Category> Categories { get; set; }
}
