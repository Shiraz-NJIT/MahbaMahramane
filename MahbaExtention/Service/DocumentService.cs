using MahbaExtention.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahbaExtention.Service
{

    public class DocumentService : IDisposable
    {
        MahbaContext _Context;
        DbSet<Document> _Document;

        public DocumentService()
        {
            _Context = new MahbaContext();
            _Document = _Context.Set<Document>();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }
        public void Add(Document doc)
        {
            _Document.Add(doc);
            _Context.SaveChanges();
        }
        public List<Document> getAll(string PN)
        {
            return  _Document.Where(q => q.PN == PN).ToList();
        }
        public List<Document> getAll()
        {
            return _Document.ToList();
        }
    }
}
