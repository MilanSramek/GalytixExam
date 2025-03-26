namespace Domain;

public interface IReadOnlyRepository<TEntity> : IQueryable<TEntity>
{
}
