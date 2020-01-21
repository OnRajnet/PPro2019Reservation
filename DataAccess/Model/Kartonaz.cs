using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    public class Kartonaz : IEntity, ISelectable
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Zadejte označení")]
        public virtual string Oznaceni { get; set; }
        [Required(ErrorMessage = "Zadejte rozměr")]
        public virtual string Rozmer { get; set; }
        [Required(ErrorMessage = "Zadejte počet kusů")]
        public virtual int Pocet { get; set; }

        public virtual bool IsSelected { get; set; }

        public override string ToString()
        {
            return Oznaceni;
        }
    }
}
