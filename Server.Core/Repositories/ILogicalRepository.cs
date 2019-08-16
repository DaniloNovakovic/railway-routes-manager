namespace Server.Core
{
    public interface ILogicalRepository<TEntity> : IRepository<TEntity> where TEntity : class, ILogical
    {
        void Resurrect(params object[] keyValues);
    }
}