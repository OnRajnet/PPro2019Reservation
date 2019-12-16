using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Rezervace: IEntity
    {
        public virtual int Id { get; set; }
        public virtual RapapUser User { get; set; }
        public virtual Lepenka Lepenka { get; set; }
        public virtual Kartonaz Kartonaz { get; set; }
        public virtual DateTime Datum { get; set; }

    }
}
