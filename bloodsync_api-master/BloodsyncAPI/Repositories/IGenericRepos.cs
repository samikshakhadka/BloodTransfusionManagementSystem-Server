using System.Linq.Expressions;

namespace BloodsyncAPI.Repositories
{
    public interface IGenericRepos
    {
        Task<List<T>> GetAll<T>(Expression<Func<T, bool>> ForUser) where T : class;
        Task<T?> GetById<T>(Guid id) where T : class;
        Task<T> GetOneById<T>(Expression<Func<T, bool>> deleted) where T : class;

        Task<T> Create<T>(T data) where T : class;
        Task<T> Update<T>(Guid id, T tobj) where T : class;


    }
}

