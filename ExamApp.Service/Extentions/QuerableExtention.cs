using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamApp.Service.Extentions
{
    public static class QueryableExtentions
    {
        public static IEnumerable<T> GetPagination<T>(this IEnumerable<T> query, Tuple<int, int> pagination = null)
            => pagination is not null ? query.Skip((pagination.Item2 - 1) * pagination.Item1) : query;

    }
}
