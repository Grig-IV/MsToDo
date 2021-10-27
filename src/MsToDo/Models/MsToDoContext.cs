using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace MsToDo.Models
{
    internal class MsToDoContext : DbContext
    {
        public DbSet<ToDoTask> Tasks { get; set; }

        public string DbPath { get; private set; }

        public MsToDoContext(string dbFileName)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{Path.DirectorySeparatorChar}{dbFileName}";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
