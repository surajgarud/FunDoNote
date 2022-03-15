using Microsoft.EntityFrameworkCore;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.context
{
   public class FunDoContext : DbContext
    {
        public FunDoContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<userEntity> User { get; set; }
        public DbSet<NotesEntity> Notes { get; set; }
    }

}
