using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class RezervaceDao : DaoBase<Rezervace>
    {
        public RezervaceDao() : base() {
        }


        public IList<Rezervace> GetRezervaceLists(int count, int page, string druh, out int totalRezervace)
        {

            totalRezervace = session.CreateCriteria<Rezervace>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            if (string.IsNullOrEmpty(druh))
                return session.CreateCriteria<Rezervace>()
                .AddOrder(Order.Asc("Id"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Rezervace>();

            return session.CreateCriteria<Rezervace>()
                .AddOrder(Order.Asc("Id"))
                //.Add(Restrictions.EqProperty("Typ", druh))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Rezervace>();
        }
        public IList<Poptavka> SearchRezervace(string phrase)
        {


            return session.CreateCriteria<Poptavka>()
                .Add(Restrictions.Like("Jmeno", string.Format("%{0}%", phrase)))

                .List<Poptavka>();

        }
    }

    //dodělat vse
}
