using Expansions;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistence;

internal sealed class ListAsyncProvider<T> : IAsyncQueryProvider
{
    private readonly List<T> _data = [];
    private readonly Func<CancellationToken, Task<List<T>>> _getData;

    public Expression BasicExpression => _data.AsQueryable().Expression;

    public ListAsyncProvider(Func<CancellationToken, Task<List<T>>> getData)
    {
        _getData = getData ?? throw new ArgumentNullException(nameof(getData));
    }

    public IQueryable CreateQuery(Expression expression)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new ListAsyncQueryable<TElement>(expression, this);
    }

    public object? Execute(Expression expression)
    {
        throw new NotImplementedException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        throw new NotImplementedException();
    }

    public async Task<TResult> ExecuteAsync<TResult>(Expression expression,
        CancellationToken cancellationToken)
    {
        if (expression is not MethodCallExpression methodCall)
        {
            throw new NotImplementedException("Expression cannot be translated.");
        }

        var toListMethod = GetMethodInfo(Enumerable.ToList, default(IEnumerable<T>)!);
        if (methodCall.Method != toListMethod)
        {
            throw new NotImplementedException("Expression cannot be translated.");
        }

        var list = await _getData(cancellationToken);
        _data.AddRange(list);

        return expression == BasicExpression
            ? (TResult)(object)list
            : _data.AsQueryable().Provider.Execute<TResult>(expression);
    }

    private static MethodInfo GetMethodInfo<T1, T2>(Func<T1, T2> func, T1 _1) => func
        .GetMethodInfo();
}
