namespace dbOperations.Model{
    
public interface IDbService
    {
        Task<List<FilmModel>> GetFilmAsync();
    }
}
