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

        [Required(ErrorMessage ="Zadejte název lepenky")]
        public virtual string Nazev { get; set; }
        [Required(ErrorMessage = "Zadejte rozměr lepenky")]
        public virtual string Rozmer { get; set; }
        [Required(ErrorMessage = "Zadejte gramáž lepenky")]
        public virtual int Gramaz { get; set; }
        [Required(ErrorMessage = "Zadejte váhu lepenky")]
        public virtual int Vaha { get; set; }
        [Required(ErrorMessage = "Zadejte kvalitu lepenky")]
        public virtual LepenkaKvalita Kvalita { get; set; }
        public virtual bool IsSelected { get; set; }
        public override string ToString()
        {
            return Nazev;
        }
    }
}
