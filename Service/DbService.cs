
using dbOperations.Database;
using dbOperations.Model;
using Microsoft.EntityFrameworkCore;

namespace dbAPI.Service{
    public class DbService : IDbService
    {
        private MysqlDbContext _dbContext;

        public DbService(MysqlDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<FilmModel>> GetFilmAsync(){
            return await _dbContext.Film.ToListAsync();
        }
    }
}