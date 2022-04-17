using System.Linq;
using System.Linq.Dynamic.Core;

namespace ApplicationFMS.Helpers
{
    public class Tools
    {

        public static IQueryable<T> ArrangeList<T>(IQueryable<T> query, string SortColumn, bool IsAscending, int take, int PageNumber) where T : class
        {
            //Ordering
            string sortColumnDirection = IsAscending ? "ascending" : "descending";
            //check for errors make first latter capital
            query = query.OrderBy(SortColumn + " " + sortColumnDirection);

            //Pagination and Calling the query
            int skip = (PageNumber - 1) * take;
            query = query.Skip(skip).Take(take);

            return query;
        }

    }
}
