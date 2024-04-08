using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure
{
    public partial class ConsoleBookStoreContextToBook : DbContext
    {
        public ConsoleBookStoreContextToBook()
        {
        }
        public ConsoleBookStoreContextToBook(DbContextOptions<ConsoleBookStoreContextToBook> options)
            : base(options)
        {
        }
        public virtual DbSet<Book> Books { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=ConsoleBookStore.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<Book>()
            .HasKey(b => b.Book_ID); // Вказання, що Book_ID є первинним ключем
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}