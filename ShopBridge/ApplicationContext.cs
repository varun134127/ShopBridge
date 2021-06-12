using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopBridge
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.Initialize(true);
            Database.CreateIfNotExists();
        }

        public DbSet<ShopFields> productinfos { get; set; }
       
    }
}