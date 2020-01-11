using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class KartonazDao : DaoBase<Kartonaz>
    {

        public KartonazDao() : base(){
        }


        public IList<Kartonaz> GetKartonyLists(int count, int page, out int totalKartony)
        {
            totalKartony = session.CreateCriteria<Kartonaz>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<Kartonaz>()
                .AddOrder(Order.Asc("Id"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Kartonaz>();
        }

        public IList<Kartonaz> GetKartonyLists2()
        {
            return session.CreateCriteria<Kartonaz>()
                .AddOrder(Order.Asc("Id"))
                .List<Kartonaz>();
        }

        public IList<Kartonaz> SearchKartonaz(string phrase)
        {
            return session.CreateCriteria<Kartonaz>()
                .Add(Restrictions.Like("Rozmer", string.Format("%{0}%", phrase)))
                .List<Kartonaz>();
        }
    }
}

