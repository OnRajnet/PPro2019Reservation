using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
   public class Objednavka : IEntity
    {
        public virtual int Id { get; set; }
        public virtual String Oznaceni { get; set; }
        public virtual int Mnozstvi { get; set; }
        public virtual int Cena { get; set; }
        public virtual RapapUser User { get; set; }
    }
}
