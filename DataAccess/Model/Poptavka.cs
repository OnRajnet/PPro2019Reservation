using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Poptavka : IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Jméno je vyžadováno")]
        public virtual string Jmeno { get; set; }
        [Required(ErrorMessage = "Přijmení je vyžadováno")]
        public virtual string Prijmeni { get; set; }
        [Required(ErrorMessage = "Email je vyžadován")]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "Cislo je vyžadováno")]
        public virtual int Cislo { get; set; }
        [Required(ErrorMessage = "Musíte vyplnit")]
        public virtual string Zprava { get; set; }
        public virtual Oblast Typ { get; set; }


    }
}
