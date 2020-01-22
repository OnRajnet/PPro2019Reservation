using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using NHibernate.Util;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class RapapUser : MembershipUser, IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Jmeno { get; set; }
        public virtual string Prijmeni { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual RapapRole Role { get; set; }
        public override string ToString()
        {
            return Login;
        }
    }
}
