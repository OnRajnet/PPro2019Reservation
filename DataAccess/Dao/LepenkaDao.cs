using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class LepenkaDao : DaoBase<Lepenka>
    {
        public LepenkaDao() : base(){
            }


        public IList<Lepenka> GetLepenkyLists(int count, int page, out int totalLepenky) {

            totalLepenky = session.CreateCriteria<Lepenka>()
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();

            return session.CreateCriteria<Lepenka>()
                .AddOrder(Order.Asc("Id"))
                .SetFirstResult((page - 1) * count)
                .SetMaxResults(count)
                .List<Lepenka>();
        }

        public IList<Lepenka> SearchLepenky(string phrase) {


            return session.CreateCriteria<Lepenka>()
                .Add(Restrictions.Like("Rozmer", string.Format("%{0}%", phrase)))

                .List<Lepenka>();

        }


        public IList<Lepenka> GetLepenkyInKvalitaId(int id)
        {
            return session.CreateCriteria<Lepenka>()
                .CreateAlias("Kvalita", "cat")
                .Add(Restrictions.Eq("cat.Id", id))
                .List<Lepenka>();
        }


    }


}
