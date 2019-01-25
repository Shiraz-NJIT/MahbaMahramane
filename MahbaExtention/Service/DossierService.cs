using MahbaExtention.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahbaExtention.Service
{
    public class DossierService : IDisposable
    {
        MahbaContext _Context;
        public DossierService()
        {
            _Context = new MahbaContext();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }
        public bool isExistPN(string PN)
        {
            return _Context.Dossiers.Any(q => q.PN == PN);
        }
        public bool isExist(string nn)
        {
            return _Context.Dossiers.Any(q => q.NN == nn);
        }
        public Dossier getByPN(string PN)
        {
            return _Context.Dossiers.FirstOrDefault(q => q.PN == PN);
        }
        public void Add(Dossier d)
        {
            _Context.Dossiers.Add(d);
            _Context.SaveChanges();
        }
        public void Update(Dossier d)
        {
            _Context.Entry(d).State = EntityState.Modified;
            _Context.SaveChanges();
        }
        public string[] GetAllPN()
        {
            return _Context.Dossiers.Select(q => q.PN).ToArray();
        }
        public List<Dossier> GetAll()
        {
            return _Context.Dossiers.ToList();
        }
    }
}
