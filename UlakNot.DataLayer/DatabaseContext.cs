using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlakNot.Entity;

namespace UlakNot.DataLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UnCategories> Categories { get; set; }
        public DbSet<UnComments> Comments { get; set; }
        public DbSet<UnHashtags> Hashtags { get; set; }
        public DbSet<UnLike> Unlikes { get; set; }
        public DbSet<UnMessages> Messages { get; set; }
        public DbSet<UnNotes> Notes { get; set; }
        public DbSet<UnSettings> Settings { get; set; }
        public DbSet<UnUsers> Users { get; set; }
        public DbSet<UnFriend> Friends { get; set; }
        public DbSet<UnContact> Contacts { get; set; }
    }
}