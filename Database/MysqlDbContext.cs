using dbOperations.Model;
using Microsoft.EntityFrameworkCore;

namespace dbOperations.Database{
    public class MysqlDbContext : DbContext{
        public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options){
        }

        public DbSet<FilmModel> Film{get;set;}

    }
}