using Microsoft.EntityFrameworkCore;
using BloodsyncAPI.Data;
using System.Linq.Expressions;



namespace BloodsyncAPI.Repositories
{
    public class GenericRepos : IGenericRepos
    {
        private readonly BloodsyncAPIContext _context;

        public GenericRepos(BloodsyncAPIContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll<T>(Expression<Func<T, bool>> ForUser) where T : class
        {
            return await this._context.Set<T>().Where(ForUser).ToListAsync();

        }
        public async Task<T?> GetById<T>(Guid id) where T : class
        {
            if (_context.Set<T>() == null)
            {
                return null;
            }
            var obj = await this._context.Set<T>().FindAsync(id);

            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        public async Task<T> GetOneById<T>(Expression<Func<T, bool>> deleted) where T : class
        {
            if (_context.Set<T>() == null)
            {
                throw new Exception("Not Found");
            }

            return await this._context.Set<T>().Where(deleted).FirstOrDefaultAsync();
        }
        public async Task<T> Create<T>(T data) where T : class
        {
            await this._context.Set<T>().AddAsync(data);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
            return data;
        }
        public async Task<T> Update<T>(Guid id, T tObj) where T : class
        {
            var info = await _context.Set<T>().FindAsync(id);
            if (info == null)
            {
                throw new Exception();
            }
            _context.Set<T>().Update(info);
           try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.Message);
                throw;
            }
            return tObj;

        }



    }
}


