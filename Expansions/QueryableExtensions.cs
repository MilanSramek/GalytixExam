using System.Linq.Expressions;
using System.Reflection;

namespace Expansions;

public static class QueryableExtensions
{
    public static Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(queryable);
        var queryProvider = queryable.Provider is IAsyncQueryProvider asyncQueryProvider
            ? asyncQueryProvider
            : throw new InvalidOperationException("The query provider does not implement IAsyncQueryableProvider interface.");

        var expression = Expression.Call(
            GetMethodInfo(Enumerable.ToList, queryable),
            queryable.Expression);
        return queryProvider.ExecuteAsync<List<T>>(expression, cancellationToken);
    }

    private static MethodInfo GetMethodInfo<T1, T2>(Func<T1, T2> func, T1 _1) => func
        .GetMethodInfo();
}
