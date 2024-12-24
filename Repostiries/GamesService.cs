

namespace GameZone.Repostiries
{
    public class GamesService : IGamesService
    {
        private readonly IGenericCrudRepository<Game> _repositry;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string  _imagePath;

        public GamesService(IGenericCrudRepository<Game> repositry,
            IWebHostEnvironment webHostEnvironment
           )
        {
            _repositry = repositry;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }


        public async Task<IEnumerable<Game>> GetAll()
		{
            return  await _repositry.ExecuteQueryAsync(query =>
                query.Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device));
		}

        public async Task<Game?> GetById(int id)
        {
            var result =  await _repositry.ExecuteQueryAsync(query =>
                query
                .Where(g => g.Id == id)
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)

                );

            return result.FirstOrDefault();
        }


        public async Task Create(CreateGameFormViewModel model)
        {
            var imageName = await SaveCover(model.CoverUrl);

            var game = new Game()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryID = model.CategoryID,
                CoverUrl = imageName,
                Devices = model.SelectedDevices.Select(d => new GameDevices { DeviceID = d }).ToList()
            };
            
            await _repositry.AddAsync(game);
            await _repositry.SaveChangesAsync();


        }

        public async Task<Game?> Update(UpdateGameFormViewModel Model)
        {
            var result = await _repositry.ExecuteQueryAsync(query =>
                 query
                 .Where(g => g.Id == Model.Id)
                 .Include(g => g.Devices)
                 );
            var game = result.FirstOrDefault();


			if (game is null)
                return null;
            
            var hasNewCover = Model.CoverUrl is not null;
            var oldCover = game.CoverUrl;


            game.Name = Model.Name;
            game.Description = Model.Description;
            game.CategoryID = Model.CategoryID;
            game.Devices = Model.SelectedDevices.Select(d => new GameDevices { DeviceID = d , GameID = game.Id }).ToList();
           

            if (hasNewCover)
            {
                game.CoverUrl = await SaveCover(Model.CoverUrl);
            }

            //_repositry.Update(game);
            var effectedRows = await _repositry.SaveChangesAsync();

            if(effectedRows > 0 && hasNewCover)
            {
               await DeleteCover(oldCover);
               return game;
            }
            else
            {
                await DeleteCover(game.CoverUrl);   
            }

            return null;

        }
        public async Task<bool> Delete(int id)
        {
            var game = await _repositry.GetByIdAsync(id);
            
            if (game is null) 
                return false;

            //delete game
            var isDeleted = await _repositry.DeleteAsync(id);
            var effectedRows = await _repositry.SaveChangesAsync();

            
            if(effectedRows > 0)   
                await DeleteCover(game.CoverUrl); //delete cover

            return effectedRows > 0;
        }
         
        private async Task<string> SaveCover(IFormFile Cover)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imagePath, imageName);

            //create place for new image and dispose it
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);

            return imageName;
        }

        private async Task DeleteCover(string CoverUrl)
        {
            var cover = Path.Combine(_imagePath, CoverUrl);
            File.Delete(cover);
        }


    }
}
