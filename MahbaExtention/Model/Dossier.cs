using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahbaExtention.Model
{
    public class Dossier
    {
       
      
        [Key]
        public string PN { get; set; }
        public string NN { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string Reshte { set; get; }
        public string Maghta { set; get; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
