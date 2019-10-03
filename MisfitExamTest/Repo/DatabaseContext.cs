using Microsoft.EntityFrameworkCore;
using MisfitExamTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisfitExamTest.Repo
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {        

        }

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Information> Information { get; set; }
    }
}
