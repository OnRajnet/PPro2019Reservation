using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class ObjednavkaDao : DaoBase<Objednavka>
    {
        public ObjednavkaDao() : base() {
        }

        public IList<Objednavka> GetLObjednavkyLists(int count, int page, out int totalObjednavky)
        {

            totalObjednavky = session.CreateCriteria<Lepenka>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<Objednavka>()
                .AddOrder(Order.Asc("Id"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Objednavka>();
        }

    }
}
