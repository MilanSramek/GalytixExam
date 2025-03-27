using System.Collections;
using System.Linq.Expressions;

namespace Persistence;

internal sealed class ListAsyncQueryable<T> : IQueryable<T>
{
    public ListAsyncQueryable(ListAsyncProvider<T> provider) :
        this(provider.BasicExpression, provider)
    {
    }

    public ListAsyncQueryable(
        Expression expression,
        IQueryProvider provider)
    {
        Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public Type ElementType => typeof(T);
    public Expression Expression { get; }
    public IQueryProvider Provider { get; }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
