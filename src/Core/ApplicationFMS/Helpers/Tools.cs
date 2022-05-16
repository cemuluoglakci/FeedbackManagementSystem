using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace ApplicationFMS.Helpers
{
    public class Tools
    {

        public static IQueryable<T> ArrangeList<T>(IQueryable<T> query, string SortColumn, bool? IsAscending, int take, int PageNumber) where T : class
        {
            //Ordering
            string sortColumnDirection = IsAscending.HasValue ? (bool)IsAscending ? "ascending" : "descending": "descending";
            //check for errors make first latter capital
            query = query.OrderBy(SortColumn + " " + sortColumnDirection);

            //Pagination and Calling the query
            int skip = (PageNumber - 1) * take;
            query = query.Skip(skip).Take(take);

            return query;
        }

        public static bool BeAValidAge(DateTime? date)
        {
            if (date == null)
            {
                return true;
            }

            int currentYear = DateTime.Now.Year;
            int dobYear = (int)date?.Year;

            if (dobYear <= currentYear - 6 && dobYear > (currentYear - 120))
            {
                return true;
            }

            return false;
        }

    }
}
