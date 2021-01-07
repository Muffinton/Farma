using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Farmacity.Api.Models
{
    public class Context : DbContext
    {
        public Context() : base("DefaulConnection")
        {

        }

        public DbSet<Articulo> Articulos { get; set; }
    }
}