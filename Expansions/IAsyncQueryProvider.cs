using System.Linq.Expressions;

namespace Expansions;

public interface IAsyncQueryProvider : IQueryProvider
{
    public Task<TResult> ExecuteAsync<TResult>(Expression expression,
        CancellationToken cancellationToken);
}
