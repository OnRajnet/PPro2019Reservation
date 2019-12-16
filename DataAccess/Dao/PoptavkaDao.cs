using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class PoptavkaDao : DaoBase<Poptavka>
    {
        public PoptavkaDao() : base() {
        }

        public IList<Poptavka> GetPoptavkyLists(int count, int page, string druh, out int totalPoptavky)
        {

            totalPoptavky = session.CreateCriteria<Poptavka>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            if (string.IsNullOrEmpty(druh))
                return session.CreateCriteria<Poptavka>()
                .AddOrder(Order.Asc("Id"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Poptavka>();

            return session.CreateCriteria<Poptavka>()
                .AddOrder(Order.Asc("Id"))
                //.Add(Restrictions.EqProperty("Typ", druh))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Poptavka>();
        }

        public IList<Poptavka> SearchPoptavky(string phrase)
        {


            return session.CreateCriteria<Poptavka>()
                .Add(Restrictions.Like("Jmeno", string.Format("%{0}%", phrase)))

                .List<Poptavka>();

        }


        public IList<Poptavka> GetPoptavkyInOblastId(int id)
        {
            return session.CreateCriteria<Poptavka>()
                .CreateAlias("Typ", "cat")
                .Add(Restrictions.Eq("cat.Id", id))
                .List<Poptavka>();
        }


    }


}
