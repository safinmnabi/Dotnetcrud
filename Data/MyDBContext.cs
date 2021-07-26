using DOTNET.Models;  
using Microsoft.EntityFrameworkCore;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  

namespace DOTNET.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Users> User{ get; set; }
        public DbSet<SignIn> Logs {get; set;}
        public DbSet<Tasks> Task {get; set;}

    }
}