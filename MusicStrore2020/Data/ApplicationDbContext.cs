using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MusicStore.Models.Movie> Movies { get; set; }
        public DbSet<MusicStore.Models.Song> Songs { get; set; }
        public DbSet<MusicStore.Models.Customer> Customers { get; set; }
    }
}
