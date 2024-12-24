namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGenericCrudRepository<Game> _genericCrudRepository;
        private readonly IProjection _projection;
        private readonly IGamesService _gameService;


        public GamesController(IGenericCrudRepository<Game> genericCrudRepository, 
            IProjection projection, 
            IGamesService gameService)
        {
            _genericCrudRepository = genericCrudRepository;
            _projection = projection;
            _gameService = gameService;
        }


        public async Task<IActionResult> Index()
        {
            var gameList = await _gameService.GetAll();
            return View(gameList.ToList());
        }

        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new CreateGameFormViewModel()
            {
                Categories = _projection.ProjectToSelectedList<Category>(),
                Devices = _projection.ProjectToSelectedList<Device>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel gameFromForm)
        {
            if (!ModelState.IsValid)
            {

                gameFromForm.Categories = _projection.ProjectToSelectedList<Category>();
                gameFromForm.Devices = _projection.ProjectToSelectedList<Device>();
                return View(gameFromForm);
            }

            await _gameService.Create(gameFromForm);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var game = await _gameService.GetById(id);
            if(game is null)return NotFound();

            return View(game);
        }


        public async Task<IActionResult> Update(int id)
        {

            var game = await _gameService.GetById(id);
            if (game is null) return NotFound();
            var viewModel = new UpdateGameFormViewModel
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryID = game.CategoryID,
                SelectedDevices = game.Devices.Select(d=>d.DeviceID).ToList(),
                currentCover = game.CoverUrl,
                Categories = _projection.ProjectToSelectedList<Category>(),
                Devices = _projection.ProjectToSelectedList<Device>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateGameFormViewModel gameFromForm)
        {
            if (!ModelState.IsValid)
            {

                gameFromForm.Categories = _projection.ProjectToSelectedList<Category>();
                gameFromForm.Devices = _projection.ProjectToSelectedList<Device>();
                return View(gameFromForm);
            }
           var game = await _gameService.Update(gameFromForm);
            
            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _gameService.Delete(id);
            
            return isDeleted ? Ok() : BadRequest() ;
        }



    }
}
