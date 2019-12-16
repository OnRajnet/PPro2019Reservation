using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using System.IO;

namespace DataAccess
{
    public class NHibernateHelper
    {

        private static ISessionFactory _factory;

        public static ISession Session
        {
            get
            {
                if (_factory == null)
                {
                    var cfg = new Configuration();
                    _factory =
                        cfg.Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hibernate.cfg.xml"))
                            .BuildSessionFactory();
                }

                return _factory.OpenSession();
            }
        }
    }
}
