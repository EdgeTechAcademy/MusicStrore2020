using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStrore2020.Models;

namespace MusicStrore2020.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MusicStrore2020.Models.Movie> Movies { get; set; }
        public DbSet<MusicStrore2020.Models.Song> Songs { get; set; }
    }
}
