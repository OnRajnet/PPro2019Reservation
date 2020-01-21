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

        [Required(ErrorMessage = "Zadejte jméno")]
        public virtual string Jmeno { get; set; }
        [Required(ErrorMessage = "Zadejte příjmení")]
        public virtual string Prijmeni { get; set; }
        [Required(ErrorMessage = "Zadejte e-mail")]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "Zadejte telefonní číslo")]
        public virtual int Cislo { get; set; }
        [Required(ErrorMessage = "Vyplňte zprávu")]
        public virtual string Zprava { get; set; }
        public virtual Oblast Typ { get; set; }


    }
}
