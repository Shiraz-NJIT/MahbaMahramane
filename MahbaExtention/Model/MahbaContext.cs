using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahbaExtention.Model
{
    public class MahbaContext : DbContext
    {
        public MahbaContext()
            : base(ConnectionString.DbString)
        {
        }

       
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Dossier> Dossiers { get; set; }
    }
}
