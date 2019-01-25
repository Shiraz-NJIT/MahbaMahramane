using MahbaExtention.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahbaExtention
{
    public class Document
    {
        public long ID { get; set; }
        public string PN { get; set; }
        public string FileName { get; set; }
        public bool AttachedToDossier { get; set; }
        public Nullable<long> ParentDocumentID { get; set; }

        public virtual Dossier Dossier { get; set; }


    }
}
