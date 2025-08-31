using FilmsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Data
{
    public class FilmContext : DbContext
    {
        public FilmContext(DbContextOptions<FilmContext> opts) : base(opts) 
        {
             
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
