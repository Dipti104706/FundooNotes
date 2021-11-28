using FundooModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Context
{
    public class UserContext : DbContext //class -helps to connect with the DB
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<CollaboratorModel> Collabs { get; set; }
        public DbSet<LabelModel> Labels { get; set; }
    }
}
