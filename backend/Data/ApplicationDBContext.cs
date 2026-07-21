using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    // Its is giant class, that will allow us to search our individual tables
    public class ApplicationDBContext : DbContext
    {
        // constructor,
        // the base is passing all the data to the DbContext
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        // It will return us the data in which manner we want 
        // using DbSet we are just manipulating the whole database set, whole table 
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<ExcelUpload> ExcelUploads { get; set; }
        public DbSet<ExcelRow> ExcelRows { get; set; }
        public DbSet<ExcelCells> ExcelCells { get; set; }

    }
}