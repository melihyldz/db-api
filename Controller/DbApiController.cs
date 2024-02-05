using dbAPI.Service;
using dbOperations.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace dbOperations.Controller
{   
    [ApiController]
    [Route("[controller]")]
    public class DbApiController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly DbService _dbService;

        public DbApiController(DbService myService,IDistributedCache cache)
        {
            _dbService = myService ?? throw new ArgumentNullException(nameof(myService));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("/films")]
        public async Task<IActionResult> GetAllData(){

            if(await _dbService.GetFilmAsync() == null)
                return Ok("boş ya da hata var");

            var films = await _dbService.GetFilmAsync();
            return Ok(films);
        }


        [HttpGet("/CacheIsNull/{key}")]
        public async Task<IActionResult> CacheIsNull([FromRoute] string key)
        {
            var existingValue = _cache.GetString(key);
            if (existingValue == null)
            {
                var newValue = "Hello, Redis yeni değer değil";
                //_cache.SetString(key, newValue);
                return Ok("Cache de veri yok");
            }
            return Ok("Cache de veri var");
        }

        [HttpGet("/GetFromCache/{key?}")]
        public async Task<IActionResult> GetFromCache([FromRoute]string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            if (cachedData == null)
                return Ok($"Belirtilen anahtarla ({key}) eşleşen veri Redis Cache'te bulunamadı.");

            var cachedList = JsonConvert.DeserializeObject<List<FilmModel>>(cachedData);

            return Content(cachedData,"application/json");
        }


        [HttpGet("DeleteCacheByKey/{key?}")]
        public async Task<IActionResult> DeleteCacheByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return Ok("Key cannot be empty");
            _cache.Remove(key);
            return Ok($"Cache with {key} cleared");
        }



        [HttpGet("AddCacheByKey/{key?}")]
        public async Task<IActionResult> AddCacheByKey(string key)
        {
            var films = await _dbService.GetFilmAsync();

            var serializedData = JsonConvert.SerializeObject(films);
            await _cache.SetStringAsync(key, serializedData);


            await _cache.SetStringAsync(key, serializedData);
            return Ok($"{key} key olarak kullanılarak veri cache eklendi");
        }
    }
}
