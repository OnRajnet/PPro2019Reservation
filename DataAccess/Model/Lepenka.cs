using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Lepenka : IEntity, ISelectable
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage ="Název lepenky je vyžadován")]
        public virtual string Nazev { get; set; }
        [Required(ErrorMessage = "Rozměr lepenky je vyžadován")]
        public virtual string Rozmer { get; set; }
        [Required(ErrorMessage = "Gramáž lepenky je vyžadována")]
        public virtual int Gramaz { get; set; }
        public virtual LepenkaKvalita Kvalita { get; set; }

        public virtual bool IsSelected { get; set; }
    }
}
