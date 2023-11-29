
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace FeastFit.Models
{
    public class FeastFitDbContext : IdentityDbContext
    {
        private string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Todor\\Documents\\CuisineLab.mdf;Integrated Security=True;Connect Timeout=30";

        public DbSet<Recipe> recipes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(connString);
        }

    }

}
